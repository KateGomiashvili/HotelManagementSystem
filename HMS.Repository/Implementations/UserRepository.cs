

using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository.Implementations
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public async Task Update(ApplicationUser entity)
        {
            var entityFromDb = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (entityFromDb is not null) 
            {
                entityFromDb.FirstName = entity.FirstName;
                entityFromDb.LastName = entity.LastName;
                entityFromDb.PhoneNumber = entity.PhoneNumber;
                if (!string.IsNullOrEmpty(entityFromDb.Email))
                {
                    entityFromDb.Email = entity.Email;
                }
            }
        }
    }
}
