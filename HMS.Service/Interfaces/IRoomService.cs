using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Rooms;

namespace HMS.Service.Interfaces
{
    public interface IRoomService
    {
        //Task<List<RoomForGettingDto>> GetMultipleRooms(int pageNumber, int pageSize);
        Task<List<RoomForGettingDto>> GetRoomsByHotelIdAsync(int hotelId, int pageNumber, int pageSize);
        Task<RoomForGettingDto> GetSingleRoom(int teacherId);
        Task AddNewRoom(RoomForCreatingDto roomForCreatingDto);
        Task UpdateRoom(RoomForUpdateDto roomForUpdatingDto);
        Task DeleteRoom(int roomId);
        Task SaveRoom();
    }
}
