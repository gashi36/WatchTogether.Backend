using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;
using WatchTogether.Backend.GraphQL.Data;

namespace GraphQL.Queries
{
    public class RoomQueries
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<Room> GetRooms(ApplicationDbContext dbContext) =>
            dbContext.Rooms.AsNoTracking();
    }
}