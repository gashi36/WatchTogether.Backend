using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Inputs;
using GraphQL.Models;
using GraphQL.Payloads;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using WatchTogether.Backend.GraphQL.Data;

namespace GraphQL.Mutations
{
    public class RoomMutations
    {
        public async Task<AddRoomPayload> AddRoomAsync(
            AddRoomInput input,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            int currentUserId = 1; // Replace with actual logged-in user ID

            var owner = await dbContext.Users.FindAsync(currentUserId);
            if (owner == null)
            {
                owner = new User { Username = "GuestUser" };
                dbContext.Users.Add(owner);
                await dbContext.SaveChangesAsync(cancellationToken);
            }


            var room = new Room
            {
                Name = input.Name,
                VideoUrl = input.VideoUrl ?? string.Empty,
                OwnerId = currentUserId,
                Owner = owner,
                InviteCode = Guid.NewGuid().ToString("N"),
                Users = new List<User> { owner } // Add owner to room
            };

            dbContext.Rooms.Add(room);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new AddRoomPayload(room);
        }


        public async Task<UpdateRoomPayload> UpdateRoomAsync(
            UpdateRoomInput input,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms.FindAsync(new object[] { input.Id }, cancellationToken);

            if (room == null)
                return new UpdateRoomPayload(null, "Room not found");

            room.Name = input.Name;
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateRoomPayload(room);
        }

        public async Task<DeleteRoomPayload> DeleteRoomAsync(
            int id,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms.FindAsync(new object[] { id }, cancellationToken);

            if (room == null)
                return new DeleteRoomPayload(false, "Room not found");

            dbContext.Rooms.Remove(room);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteRoomPayload(true);
        }


        public async Task<string> InviteUserAsync(
            InviteUserInput input,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms.FindAsync(new object?[] { input.RoomId }, cancellationToken);
            if (room == null)
                throw new Exception("Room not found.");

            // Example frontend join link
            string baseUrl = "https://yourdomain.com/join/";
            string inviteLink = $"{baseUrl}{room.InviteCode}";

            return inviteLink;
        }

        public async Task<JoinRoomPayload> JoinRoomByCodeAsync(
            string inviteCode,
            string? guestName, // optional guest
            int? userId,       // optional user
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            // Find room by invite code
            var room = await dbContext.Rooms
                .Include(r => r.Users)
                .Include(r => r.Guests)
                .FirstOrDefaultAsync(r => r.InviteCode == inviteCode, cancellationToken);

            if (room == null)
                throw new Exception("Invalid invite code.");

            User? user = null;
            Guest? guest = null;

            if (userId.HasValue) // logged-in user
            {
                user = await dbContext.Users.FindAsync(new object[] { userId.Value }, cancellationToken);
                if (user == null)
                    throw new Exception("User not found.");

                if (!room.Users.Contains(user))
                    room.Users.Add(user);
            }
            else // guest
            {
                string guestId = Guid.NewGuid().ToString("N");
                guest = new Guest
                {
                    GuestId = guestId,
                    Name = guestName ?? "Guest",
                    Room = room
                };
                room.Guests.Add(guest);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return new JoinRoomPayload(room, user, guest);
        }

        public async Task<Room> UpdateVideoStateAsync(
    int roomId,
    double currentTime,
    bool isPlaying,
    ApplicationDbContext dbContext,
    [Service] ITopicEventSender eventSender,
    CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms.FindAsync(roomId);
            if (room == null) throw new Exception("Room not found");

            room.CurrentTime = currentTime;
            room.IsPlaying = isPlaying;

            await dbContext.SaveChangesAsync(cancellationToken);

            // Broadcast new state to all subscribers in this room
            await eventSender.SendAsync($"Room_{roomId}_Video", room, cancellationToken);

            return room;
        }


    }
}