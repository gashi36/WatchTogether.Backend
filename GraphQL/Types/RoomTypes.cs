using GraphQL.Models;
using HotChocolate.Types;

public class RoomTypes : ObjectType<Room>
{
    protected override void Configure(IObjectTypeDescriptor<Room> descriptor)
    {
        descriptor.Field(r => r.Id).Type<NonNullType<IdType>>();
        descriptor.Field(r => r.Name).Type<NonNullType<StringType>>();
    }
}
