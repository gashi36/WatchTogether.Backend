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
    public class MessageByIdDataLoader : BatchDataLoader<int, Message>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public MessageByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Message>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            var messages = await dbContext.Messages
                .AsNoTracking()
                .Where(m => keys.Contains(m.Id))
                .ToListAsync(cancellationToken);

            return messages.ToDictionary(m => m.Id);
        }
    }
}
