using HotChocolate;
using HotChocolate.Subscriptions;
using GraphQL.Models;

namespace WatchTogether.Backend.GraphQL.Subscriptions
{
    public class MessageSubscriptions
    {
        [Subscribe]
        [Topic("Room_{roomId}_Messages")]
        public Message OnMessageAdded([EventMessage] Message message) => message;
    }
}
