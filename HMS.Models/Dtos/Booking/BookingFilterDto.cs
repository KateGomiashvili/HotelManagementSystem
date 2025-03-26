using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Dtos.Booking
{
    public class BookingFilterDto
    {
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
        public string? GuestId { get; set; }
        public DateTime? CheckInDateFrom { get; set; }
        public DateTime? CheckInDateTo { get; set; }
        public DateTime? CheckOutDateFrom { get; set; }
        public DateTime? CheckOutDateTo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
