using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Dtos.Booking
{
    public class BookingForCreatingDto
    {
        
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }


        public string GuestId { get; set; }

        public int RoomId { get; set; }
    }
}
