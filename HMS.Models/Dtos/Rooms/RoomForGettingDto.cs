using HMS.Models.Dtos.Booking;
using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Dtos.Rooms
{
    public class RoomForGettingDto
    {
        public int Id { get; set; }

        public string RoomType { get; set; }

        public bool IsAvailable { get; set; } = true;
        public decimal Price { get; set; }

        public int HotelId { get; set; }
        //public Hotel Hotel { get; set; }
        public List<BookingForGettingDto> Bookings { get; set; }
    }
}
