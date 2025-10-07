using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTogether.Backend.GraphQL.Inputs
{
    public sealed record AddUserInput(
           string Username,
           string Password
       );
    public record UpdateUserInput(int Id, string Username);
    public record ChangePasswordInput(int UserId, string NewPassword);
}