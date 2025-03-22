using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        
        public string RoomType { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        public decimal Price { get; set; }
        
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public List<Booking> Bookings { get; set; }

    }
}
