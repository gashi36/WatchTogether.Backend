using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Models
{
    public class Guest
    {
        [Key]
        public string GuestId { get; set; } = default!; // unique ID for guest
        public string? Name { get; set; }

        // Optional: reference to the Room
        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}