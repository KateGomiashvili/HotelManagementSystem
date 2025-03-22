using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Dtos.Hotels
{
    public class HotelForUpdatingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public string Address { get; set; }
       
    }
}
