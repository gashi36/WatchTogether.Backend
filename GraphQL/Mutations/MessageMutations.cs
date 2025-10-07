using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Inputs;
using GraphQL.Models;
using GraphQL.Payloads;
using WatchTogether.Backend.GraphQL.Data;
using HotChocolate;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Mutations
{
    public class MessageMutations
    {
        public async Task<Message> AddMessageAsync(
            AddMessageInput input,
            ApplicationDbContext dbContext,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Content = input.Content,
                RoomId = input.RoomId,
                CreatedAt = DateTime.UtcNow
            };

            // If user is logged in
            if (input.UserId.HasValue)
            {
                var user = await dbContext.Users.FindAsync(input.UserId.Value);
                if (user == null)
                    throw new Exception("User not found.");

                message.UserId = user.Id;
            }
            // Otherwise treat as guest
            else if (!string.IsNullOrEmpty(input.GuestName))
            {
                // Create guest with unique ID
                var guest = new Guest
                {
                    GuestId = Guid.NewGuid().ToString("N"),
                    Name = input.GuestName
                };

                // Add to DB first
                dbContext.Guests.Add(guest);
                await dbContext.SaveChangesAsync(cancellationToken);

                // Link message to guest
                message.GuestId = guest.GuestId;
            }
            else
            {
                throw new Exception("Either UserId or GuestName must be provided.");
            }

            // Save message
            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Publish message to subscribers
            await eventSender.SendAsync(input.RoomId.ToString(), message, cancellationToken);

            return message;
        }


    }
}
