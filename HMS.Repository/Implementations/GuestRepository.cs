using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository.Implementations
{
    public class GuestRepository : RepositoryBase<Guest>, IGuestRepository
    {
        private readonly ApplicationDbContext _context;
        public GuestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Save() => await _context.SaveChangesAsync();
        public async Task Update(Guest entity)
        {

        }
    }
}
