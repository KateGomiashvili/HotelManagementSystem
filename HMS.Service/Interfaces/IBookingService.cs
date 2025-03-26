using HMS.Models.Dtos.Booking;
using HMS.Models.Dtos.Rooms;


namespace HMS.Service.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingForGettingDto>> GetReservationsByRoomIdAsync(int hotelId, int pageNumber, int pageSize);
        Task<List<BookingForGettingDto>> GetReservationsByFilter(
            string? guestId,
            int? roomId,
            DateTime? checkInDate,
            DateTime? checkOutDate,
            int pageNumber,
            int pageSize);
        Task<BookingForGettingDto> GetSingleReservation(int bookingId);
        
        Task AddNewReservation(BookingForCreatingDto bookingForCreatingDto);
        Task UpdateReservation(BookingForUpdatingDto bookingForUpdatingDto);
        Task DeleteReservation(int bookingId);
        Task SaveBooking();
    }
}
