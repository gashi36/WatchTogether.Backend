using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Models;

namespace GraphQL.Payloads
{
    // Message payloads
    public sealed class AddMessagePayload(Message message)
    {
        public Message Message { get; } = message;
    }

    public sealed class UpdateMessagePayload(Message? message, string? errorMessage = null)
    {
        public Message? Message { get; } = message;
        public string? ErrorMessage { get; } = errorMessage;
    }

    public sealed class DeleteMessagePayload(bool success, string? errorMessage = null)
    {
        public bool Success { get; } = success;
        public string? ErrorMessage { get; } = errorMessage;
    }
}