using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Models;

namespace WatchTogether.Backend.GraphQL.Payloads
{
    // Payloads for User mutations
    public sealed class AddUserPayload(User user)
    {
        public User User { get; } = user;
    }

    public sealed class UpdateUserPayload(User? user, string? errorMessage = null)
    {
        public User? User { get; } = user;
        public string? ErrorMessage { get; } = errorMessage;
    }

    public sealed class DeleteUserPayload(bool success, string? errorMessage = null)
    {
        public bool Success { get; } = success;
        public string? ErrorMessage { get; } = errorMessage;
    }

    public sealed class ChangePasswordPayload(bool success, string? errorMessage = null)
    {
        public bool Success { get; } = success;
        public string? ErrorMessage { get; } = errorMessage;
    }

}