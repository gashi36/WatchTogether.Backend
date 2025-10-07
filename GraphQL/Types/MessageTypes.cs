using GraphQL.Models;
using HotChocolate.Types;

public class MessageTypes : ObjectType<Message>
{
    protected override void Configure(IObjectTypeDescriptor<Message> descriptor)
    {
        descriptor.Field(m => m.Id).Type<NonNullType<IdType>>();
        descriptor.Field(m => m.Content).Type<NonNullType<StringType>>();
        descriptor.Field(m => m.UserId).Ignore(); // resolved via DataLoader
        descriptor.Field(m => m.RoomId).Ignore(); // resolved via DataLoader
    }
}
