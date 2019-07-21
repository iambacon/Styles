using System;

namespace IAmBacon.Core.Application.User.Commands
{
    public class CreateUserCommand
    {
        public string Bio { get; }
        public string ProfileImage { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        public CreateUserCommand(string firstName, string lastName, string email, string profileImage, string bio)
        {
            FirstName = !string.IsNullOrWhiteSpace(firstName)
                ? firstName
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(firstName));

            LastName = !string.IsNullOrWhiteSpace(lastName)
                ? lastName
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(lastName));

            Email = !string.IsNullOrWhiteSpace(email)
                ? email
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));

            ProfileImage = !string.IsNullOrWhiteSpace(profileImage)
                ? profileImage
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(profileImage));

            Bio = !string.IsNullOrWhiteSpace(bio)
                ? bio
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(bio));
        }
    }
}
