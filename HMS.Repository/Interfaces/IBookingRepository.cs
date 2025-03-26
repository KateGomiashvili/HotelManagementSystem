

using HMS.Models.Entities;

namespace HMS.Repository.Interfaces
{
    public interface IBookingRepository : IRepositoryBase<Booking> , IUpdatable<Booking>, ISavable
    {
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate);
    }
}
