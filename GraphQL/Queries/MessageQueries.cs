using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;
using WatchTogether.Backend.GraphQL.Data;

namespace GraphQL.Queries
{
    public class MessageQueries
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<Message> GetMessages(ApplicationDbContext dbContext) =>
            dbContext.Messages.AsNoTracking();
        
        
        public IQueryable<Message> GetMessagesByRoom(int roomId, [Service] ApplicationDbContext dbContext)
        {
            return dbContext.Messages.Where(m => m.RoomId == roomId);
        }
    }
    
}