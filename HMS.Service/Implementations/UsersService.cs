using AutoMapper;
using HMS.Models.Entities;
using HMS.Repository.Data;
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
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGuestRepository _guestRepository;
        private readonly IManagerRepository _managerRepository;
        public UsersService(ApplicationDbContext context, IGuestRepository guestRepository, IManagerRepository managerRepository)
        { 
            _context = context;
            _guestRepository = guestRepository;
            _managerRepository = managerRepository;
        }
        public async Task DeleteGuest(string guestId)
        {
            var guestToDelete = await _guestRepository.GetAsync(g => g.UserId == guestId);
            if (guestToDelete == null) 
            {
                throw new NotFoundException("User not found");
            }
            if (guestToDelete.Bookings.Any())
            {
                throw new ConflictException("This user has active Reservations");
            }
             _guestRepository.Remove(guestToDelete);
            await _guestRepository.Save();
        }

        public async Task DeleteManager(string managerId)
        {
            var managerToDelete = await _managerRepository.GetAsync(m => m.UserId == managerId);
            
            if (managerToDelete == null)
            {
                throw new NotFoundException("User not found");
            }
            var hotel = _context.Hotels
                .Include(h => h.Managers)
                .FirstOrDefault(h => h.Managers.Any(m => m.UserId == managerId));
            if (hotel.Managers.Count() <= 1)
            {
                throw new ConflictException("Hotel has only this Manager");
            }
            _managerRepository.Remove(managerToDelete);
            await _managerRepository.Save();
        }
    }
}
