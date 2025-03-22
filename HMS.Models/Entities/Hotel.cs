using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public float Rating { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string Address { get; set; }
        public List<Room> Rooms { get; set; } = new();
        public List<Manager> Managers { get; set; } = new();
        // public List<Booking> Bookings { get; set; } =new();

    }
}
