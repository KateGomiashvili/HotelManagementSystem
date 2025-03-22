using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Entities
{
    public class Guest
    {
        //public string UserId { get; set; } // Foreign key to ApplicationUser
        //public ApplicationUser User { get; set; }
        //public List<Booking> Bookings { get; set; } = new();
 
        public string UserId { get; set; } // Primary key and foreign key

        public ApplicationUser User { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
