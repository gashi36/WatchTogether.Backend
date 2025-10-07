using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Inputs
{
    public sealed record AddRoomInput(string Name, string? VideoUrl);

    public sealed record UpdateRoomInput(int Id, string Name);
    public sealed record JoinRoomInput(
        string InviteCode,       // the code from the invite link
        int? UserId = null,      // optional for logged-in users
        string? GuestName = null // optional for guests
    );


    public sealed record LeaveRoomInput(int RoomId, int UserId);

    public sealed record InviteUserInput(
        int RoomId,
        int? InvitedUserId = null,    // registered user
        string? InvitedGuestId = null, // guest
        string? GuestName = null
    );

}