﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models.Entities
{
    public class Booking
    {
        
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        
        public string GuestId { get; set; }
        public Guest Guest { get; set; }

        
        public int RoomId { get; set; }
        public Room Room { get; set; }

        
        

    }
}
