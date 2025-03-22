using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Dtos.Hotels
{
    public class HotelForCreatingDto
    {

        public string Name { get; set; }

        public float Rating { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
        
    }
}
