﻿using HMS.Models.Dtos.Rooms;
using HMS.Models.Entities;

namespace HMS.Repository.Interfaces
{
    public interface IRoomRepository : IRepositoryBase<Room> , IUpdatable<Room>, ISavable
    {
        //Task<bool> ExistsAsync(int requestHotelId);
        Task<List<RoomForGettingDto>> GetRoomsByHotelIdAsync(int hotelId, int pageNumber, int pageSize);
    }
}
