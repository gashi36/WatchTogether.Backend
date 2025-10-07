using System.Threading;
using System.Threading.Tasks;
using GraphQL.Models;
using HotChocolate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchTogether.Backend.GraphQL.Data;
using WatchTogether.Backend.GraphQL.Inputs;
using WatchTogether.Backend.GraphQL.Payloads;

namespace WatchTogether.Backend.GraphQL.Mutations
{
    public class UserMutations
    {
        // Add a new user
        public async Task<AddUserPayload> AddUserAsync(
            AddUserInput input,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Username = input.Username,
                PasswordHash = passwordHasher.HashPassword(null, input.Password)
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new AddUserPayload(user);
        }

        // Update username
        public async Task<UpdateUserPayload> UpdateUserAsync(
            UpdateUserInput input,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FindAsync(new object[] { input.Id }, cancellationToken);
            if (user == null)
            {
                return new UpdateUserPayload(null, "User not found.");
            }

            user.Username = input.Username;
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateUserPayload(user);
        }

        // Delete user
        public async Task<DeleteUserPayload> DeleteUserAsync(
            int id,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FindAsync(new object[] { id }, cancellationToken);
            if (user == null)
            {
                return new DeleteUserPayload(false, "User not found.");
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteUserPayload(true);
        }

        // Change user password
        public async Task<ChangePasswordPayload> ChangePasswordAsync(
            ChangePasswordInput input,
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FindAsync(new object[] { input.UserId }, cancellationToken);
            if (user == null)
            {
                return new ChangePasswordPayload(false, "User not found.");
            }

            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, input.NewPassword);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new ChangePasswordPayload(true);
        }
    }
}
