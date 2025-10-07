using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogether.Backend.GraphQL.Queries;

namespace GraphQL.Queries
{

    public class Query
    {
        public UserQueries Users([Service] UserQueries userQueries) => userQueries;
        public RoomQueries Rooms([Service] RoomQueries roomQueries) => roomQueries;
        public MessageQueries Messages([Service] MessageQueries messageQueries) => messageQueries;
    }

}