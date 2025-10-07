using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Models
{
public sealed class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // hashed password

    // Navigation
    public ICollection<Room> OwnedRooms { get; set; } = new List<Room>();

    // Many-to-many: rooms this user is member of
    public ICollection<Room> Rooms { get; set; } = new List<Room>();

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
}