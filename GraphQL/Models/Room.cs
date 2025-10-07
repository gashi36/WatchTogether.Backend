using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Models
{

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // The URL of the video being watched together
        public string? VideoUrl { get; set; }

        // The owner (creator) of the room
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
        public double CurrentTime { get; set; } = 0; // seconds
        public bool IsPlaying { get; set; } = false;

        // Messages between users in this room
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        // Registered users in the room
        public ICollection<User> Users { get; set; } = new List<User>();

        // Guest users (non-registered participants)
        public ICollection<Guest> Guests { get; set; } = new List<Guest>();

        // Optional guest ID for public access links
        public string? GuestId { get; set; } = null;
        public string InviteCode { get; set; } = Guid.NewGuid().ToString("N");

    }
}