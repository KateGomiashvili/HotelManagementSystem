using HMS.Models.Dtos.Hotels;
using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace HMS.Repository.Implementations
{
    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        private readonly ApplicationDbContext _context;
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public async Task Update(Hotel entity)
        {
            var entityFromDb = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entityFromDb is not null)
            {
                entityFromDb.Name = entity.Name;
                entityFromDb.Address = entity.Address;
                entityFromDb.Rating = entity.Rating;
            }
        }
        
    }
}
