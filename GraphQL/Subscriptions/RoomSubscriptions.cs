using GraphQL.Models;

namespace GraphQL.Subscriptions
{
    public class RoomSubscriptions
    {
        [Subscribe]
        [Topic("Room_{roomId}_Video")]
        public Room OnVideoStateChanged([EventMessage] Room room) => room;
    }
}
