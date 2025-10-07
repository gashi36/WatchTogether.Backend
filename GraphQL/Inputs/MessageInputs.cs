using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Inputs
{
    public sealed record AddMessageInput(
        string Content,
        int RoomId,
        int? UserId = null,     // logged-in user
        string? GuestId = null, // guest identifier
        string? GuestName = null // guest display name
    );


    public sealed record UpdateMessageInput(int Id, string Content);
}