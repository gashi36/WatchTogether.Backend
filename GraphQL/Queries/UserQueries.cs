using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;
using WatchTogether.Backend.GraphQL.Data;
using WatchTogether.Backend.GraphQL.DataLoaders;

namespace WatchTogether.Backend.GraphQL.Queries
{

    public class UserQueries
    {

        public async Task<IEnumerable<User>> GetUsersAsync(
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken)
        {
            return await dbContext.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public static async Task<User?> GetUserAsync(
            int id,
            UserByIdDataLoader userLoader,
            CancellationToken cancellationToken)
        {
            // Load the user by ID using the DataLoader
            return await userLoader.LoadAsync(id, cancellationToken);
        }
    }
}