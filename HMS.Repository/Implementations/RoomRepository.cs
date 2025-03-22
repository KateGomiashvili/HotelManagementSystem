using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Rooms;
using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository.Implementations
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<RoomForGettingDto>> GetRoomsByHotelIdAsync(int hotelId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task Save() => await _context.SaveChangesAsync();
        public async Task Update(Room entity)
        {
            var entityFromDb = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if(entityFromDb is not null)
            {
                entityFromDb.Price = entity.Price;
                entityFromDb.IsAvailable = entity.IsAvailable;
                entityFromDb.RoomType = entity.RoomType;
            }
        }
        
    }
}
