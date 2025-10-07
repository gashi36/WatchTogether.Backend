using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // User who sent the message
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string? GuestId { get; set; }
        public Guest? Guest { get; set; }
        // Room where the message belongs
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }
}