using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTogether.Backend.GraphQL.Mutations;

namespace GraphQL.Mutations
{
    public class Mutations
    {
        public UserMutations Users([Service] UserMutations userMutations) => userMutations;
        public RoomMutations Rooms([Service] RoomMutations roomMutations) => roomMutations;
        public MessageMutations Messages([Service] MessageMutations messageMutations) => messageMutations;
    }

}