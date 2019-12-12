using System;

namespace IAmBacon.Core.Application.User.Commands
{
    public class DeleteUserCommand
    {
        public int Id { get; }

        public string Email { get; }

        public DeleteUserCommand(int id, string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));

            Id = id;
            Email = email;
        }
    }
}
