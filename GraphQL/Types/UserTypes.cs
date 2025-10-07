using GraphQL.Models;
using HotChocolate.Types;

public class UserTypes : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(u => u.Id).Type<NonNullType<IdType>>();
        descriptor.Field(u => u.Username).Type<NonNullType<StringType>>();
        descriptor.Field(u => u.PasswordHash).Ignore(); // don’t expose passwords
    }
}
