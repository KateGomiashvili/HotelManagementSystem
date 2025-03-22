using HMS.Models.Dtos.Rooms;
using HMS.Models.Dtos.UserDto;
using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Dtos.Hotels
{
    public class HotelForGettingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
        public List<RoomForGettingDto> Rooms { get; set; } = new();
        public List<UserForGettingDto> Managers { get; set; } = new();
    }
}
