

using AutoMapper;
using HMS.Models.Dtos.Booking;
using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Implementations;
using HMS.Repository.Interfaces;
using HMS.Service.Exceptions;
using HMS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HMS.Service.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IRoomRepository _roomRepository;
        private readonly IGuestRepository _guestRepository;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper,
            ApplicationDbContext context, IRoomRepository roomRepository, IGuestRepository guestRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _context = context;
            _roomRepository = roomRepository;
            _guestRepository = guestRepository;
        }

        public async Task AddNewReservation(BookingForCreatingDto bookingForCreatingDto) //Add Reservation
        {
            if (bookingForCreatingDto is null)
            {
                throw new BadRequestException($"{bookingForCreatingDto} is an invalid argument");
            }
            if (bookingForCreatingDto.CheckInDate > bookingForCreatingDto.CheckOutDate)
            {
                throw new BadRequestException("Enter valid dates.");
            }
            var room = await _roomRepository.GetAsync(room => room.Id == bookingForCreatingDto.RoomId);
            if (room == null)
                throw new NotFoundException("Room not found");


            var guest = await _guestRepository.GetAsync(guest => guest.UserId == bookingForCreatingDto.GuestId);
            if (guest == null)
                throw new NotFoundException("Guest not found");
            bool isAvailable = await _bookingRepository.IsRoomAvailableAsync(
            bookingForCreatingDto.RoomId,
            bookingForCreatingDto.CheckInDate,
            bookingForCreatingDto.CheckOutDate);
            if (!isAvailable)
            {
                throw new ConflictException("Room is not available");
            }

            var entityData = _mapper.Map<Booking>(bookingForCreatingDto);
            await _bookingRepository.AddAsync(entityData);
        }

        public async Task DeleteReservation(int bookingId)  //Delete Reservation
        {
            if (bookingId <= 0)
            {
                throw new BadRequestException($"{bookingId} is an invalid argument");
            }

            var bookingToDelete = await _bookingRepository.GetAsync(x => x.Id == bookingId);

            if (bookingToDelete is null)
            {
                throw new NotFoundException($"Reservation with id {bookingId} not found");
            }

            _bookingRepository.Remove(bookingToDelete);
        }

        public Task<List<BookingForGettingDto>> GetReservationsByRoomIdAsync(int roomId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingForGettingDto> GetSingleReservation(int bookingId)
        {
            if (bookingId < 1)
                throw new BadRequestException($"{bookingId} is an invalid argument");
            var entityData = await _bookingRepository.GetAsync(b => b.Id==bookingId);
            if(entityData is null)
            {
                throw new NotFoundException("Reservation not found");
            }
            var mappedData = _mapper.Map<BookingForGettingDto>(entityData);
            return mappedData;
        }

        public async Task SaveBooking() => await _bookingRepository.Save();

        public async Task UpdateReservation(int bookingId, DateTime newCheckInDate, DateTime newCheckOutDate)
        {
            if (bookingId<1)
                throw new BadRequestException($"{bookingId} is an invalid argument");
            if (newCheckOutDate <= newCheckInDate)
                throw new BadRequestException("Check-out date must be after check-in date");

            var reservationToUpdate = await _context.Bookings
        .FirstOrDefaultAsync(r => r.Id == bookingId);
            if (reservationToUpdate == null)
                throw new NotFoundException("Reservation not found");
            bool hasConflict = await _context.Bookings
        .AnyAsync(r => r.RoomId == reservationToUpdate.RoomId
                    && r.Id != bookingId
                    && newCheckInDate < r.CheckOutDate
                    && newCheckOutDate > r.CheckInDate);
            if (hasConflict)
                throw new ConflictException("Room is not available");
            BookingForUpdatingDto bookingForUpdatingDto = new BookingForUpdatingDto
            {
                CheckInDate = newCheckInDate,
                CheckOutDate = newCheckOutDate
            };
            var entityData = _mapper.Map<Booking>(bookingForUpdatingDto);
            await _bookingRepository.Update(entityData);
        }
        public async Task<List<BookingForGettingDto>> GetReservationsByFilter(  //filter
            string? guestId,
            int? roomId,
            DateTime? checkInDate,
            DateTime? checkOutDate,
            int pageNumber,
            int pageSize
            )

        {
            var result = new List<BookingForGettingDto>();
            if (guestId is null && roomId is null && checkInDate is null && checkOutDate is null)
            {
                throw new BadRequestException("At least one filter (city or must be provided");
            }
            List<Booking> reservations = await _bookingRepository.GetAllAsync(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<BookingForGettingDto>>(reservations);
            var query = mappedData.AsQueryable();
            if (!string.IsNullOrWhiteSpace(guestId))
            {
                query = query.Where(r => r.GuestId == guestId);
            }
            if (roomId is not null)
            {
                query = query.Where(r => r.RoomId == roomId);
            }
            if (checkInDate is not null)
            {
                query = query.Where(r => r.CheckInDate == checkInDate);
            }
            if (checkOutDate is not null)
            {
                query = query.Where(r => r.CheckOutDate == checkInDate);
            }
            if (query.Count() > 0)
            {
                return query.ToList();
            }
            else
            {
                throw new NotFoundException("Reservations not found");
            }

        }
    }
}
