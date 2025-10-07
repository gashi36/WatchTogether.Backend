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
    public class RoomByIdDataLoader : BatchDataLoader<int, Room>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public RoomByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Room>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            var rooms = await dbContext.Rooms
                .AsNoTracking()
                .Where(r => keys.Contains(r.Id))
                .ToListAsync(cancellationToken);

            return rooms.ToDictionary(r => r.Id);
        }
    }
}
