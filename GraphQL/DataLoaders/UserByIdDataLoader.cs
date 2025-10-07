using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using WatchTogether.Backend.GraphQL.Data;

namespace WatchTogether.Backend.GraphQL.DataLoaders
{
    public class UserByIdDataLoader : BatchDataLoader<int, User>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public UserByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, User>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            var users = await dbContext.Users
                .AsNoTracking()
                .Where(u => keys.Contains(u.Id))
                .ToListAsync(cancellationToken);

            return users.ToDictionary(u => u.Id);
        }
    }
}
