using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace HMS.Repository.Implementations
{
    public class ManagerRepository : RepositoryBase<Manager>, IManagerRepository
    {
        private readonly ApplicationDbContext _context;
        public ManagerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Save() => await _context.SaveChangesAsync();
        public async Task Update(Manager entity)
        {
            var entityFromDb = await _context.Managers.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
            if (entityFromDb is not null)
            {
                entityFromDb.HotelId = entity.HotelId;
            }
            }
        }
}
