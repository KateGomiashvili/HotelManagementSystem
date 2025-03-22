using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Dtos.Rooms
{
    public class RoomForUpdateDto
    {

        public string RoomType { get; set; }

        public bool IsAvailable { get; set; } = true;
        public decimal Price { get; set; }
    }
}
