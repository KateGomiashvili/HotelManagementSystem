

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models.Entities
{
    public class Manager
    {
        public string UserId { get; set; } // Primary key and foreign key

        public ApplicationUser ApplicationUser { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
