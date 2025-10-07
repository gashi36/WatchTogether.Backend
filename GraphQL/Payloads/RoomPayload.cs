using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Models;

namespace GraphQL.Payloads
{
    // Room payloads
    public sealed class AddRoomPayload(Room room)
    {
        public Room Room { get; } = room;
    }

    public sealed class UpdateRoomPayload(Room? room, string? errorMessage = null)
    {
        public Room? Room { get; } = room;
        public string? ErrorMessage { get; } = errorMessage;
    }

    public sealed class DeleteRoomPayload(bool success, string? errorMessage = null)
    {
        public bool Success { get; } = success;
        public string? ErrorMessage { get; } = errorMessage;
    }
    public sealed record JoinRoomPayload(Room Room, User? User = null, Guest? Guest = null);




    public sealed record LeaveRoomPayload(Room Room, User User);
    public sealed class InviteUserPayload(Room room, User? user = null, Guest? guest = null)
    {
        public Room Room { get; } = room;
        public User? User { get; } = user;
        public Guest? Guest { get; } = guest;
    }



}