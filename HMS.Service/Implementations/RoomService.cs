using AutoMapper;
using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Rooms;
using HMS.Models.Entities;
using HMS.Repository.Implementations;
using HMS.Repository.Interfaces;
using HMS.Service.Exceptions;
using HMS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Implementations
{
    public class RoomService : IRoomService

    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IHotelService _hotelService;

        //private readonly IImageService _imageService;

        public RoomService(IRoomRepository roomRepository, IMapper mapper, IHotelService hotelService)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _hotelService = hotelService;
            // _imageService = imageService;
        }
        public async Task AddNewRoom(RoomForCreatingDto roomForCreatingDto)
        {
            if (roomForCreatingDto is null)
            {
                throw new BadRequestException($"{roomForCreatingDto} is an invalid argument");
            }
            var requestHotelId = roomForCreatingDto.HotelId;
            if(!await _hotelService.ExistsAsync(requestHotelId))
            {
                throw new NotFoundException($"Hotel not found");
            }
            var entityData = _mapper.Map<Room>(roomForCreatingDto);
            await _roomRepository.AddAsync(entityData);
        }

        public async Task DeleteRoom(int roomId)  //delete room
        {
            if (roomId <= 0)
                throw new BadRequestException($"{roomId} is an invalid argument");
            var roomToDelete = await _roomRepository.GetAsync(r => r.Id == roomId);
            if (roomToDelete is null)
            {
                throw new NotFoundException("Room not found");
            }
            
            bool hasBookings = roomToDelete.Bookings?.Any(b => b.CheckOutDate >= DateTime.Today) ?? false;
            //rooms.Any(r =>
            //    r.Bookings?.Any(b => b.CheckOutDate >= DateTime.Today) ?? false);
            if (hasBookings)
            {
                throw new ConflictException("Unable to delete Hotel with active reservations");
            }
            _roomRepository.Remove(roomToDelete);
        }

        //public async Task<List<RoomForGettingDto>> GetMultipleRooms(int pageNumber, int pageSize)
        //{
        //    List<Room> entityData = await _roomRepository.GetAllAsync(pageNumber, pageSize, includeProperties: "Bookings");
        //    List<RoomForGettingDto> result = new();

        //    if (entityData.Any())
        //    {
        //        var mappedData = _mapper.Map<List<RoomForGettingDto>>(entityData);
        //        result.AddRange(mappedData);
        //        return result;
        //    }
        //    else
        //    {
        //        throw new NotFoundException($"Rooms not found");
        //    }
        //}

        public async Task<List<RoomForGettingDto>> GetRoomsByHotelIdAsync(int hotelId, int pageNumber, int pageSize)
        {
            List<Room> entityData = await _roomRepository.GetAllAsync(r=> r.HotelId==hotelId, pageNumber, pageSize);
            List<RoomForGettingDto> result = new();

            if (entityData.Any())
            {
                var mappedData = _mapper.Map<List<RoomForGettingDto>>(entityData);
                result.AddRange(mappedData);
                return result;
            }
            else
            {
                throw new NotFoundException($"Rooms not found");
            }
        }

        //public async Task<List<RoomForGettingDto>> GetRoomsByHotelIdAsync(int hotelId, int pageNumber, int pageSize)
        //{
        //    List<Room> entityData = await _roomRepository.GetAllAsync(r => r.HotelId == hotelId, pageNumber, pageSize);
        //    List<RoomForGettingDto> result = new();
        //    if (entityData.Any())
        //    {
        //        var mappedData = _mapper.Map<List<RoomForGettingDto>>(entityData);
        //        result.AddRange(mappedData);
        //        return result;
        //    }
        //    else
        //    {
        //        throw new NotFoundException($"Rooms not found");
        //    }
        //}

        public Task<RoomForGettingDto> GetSingleRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveRoom() => await _roomRepository.Save();

        public async Task UpdateRoom(RoomForUpdateDto roomForUpdatingDto)
        {
            if (roomForUpdatingDto is null)
                throw new BadRequestException($"{roomForUpdatingDto} is an invalid argument");

            var entityData = _mapper.Map<Room>(roomForUpdatingDto);
            await _roomRepository.Update(entityData);
        }
    }
}
