﻿using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository.Implementations
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Save() => await _context.SaveChangesAsync();
        public async Task Update(Booking entity)
        {
            var entityFromDb = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (entityFromDb is not null) 
            { 
                entityFromDb.CheckOutDate = entity.CheckOutDate;
                entityFromDb.CheckInDate = entity.CheckInDate;
            }
        }
        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var conflictingReservations = await _context.Bookings
                .Where(r => r.RoomId == roomId)
                .Where(r => (checkOutDate > r.CheckInDate && checkInDate < r.CheckOutDate))
                .AnyAsync();

            return !conflictingReservations;
        }
    }
}
