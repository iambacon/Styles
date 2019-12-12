using System;

namespace IAmBacon.Core.Application.User.Commands
{
    public class UpdateUserCommand
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public UpdateUserCommand(int id, string bio, string profileImage, string firstName, string lastName, string email, bool active,
            bool deleted)
        {
            if (string.IsNullOrWhiteSpace(bio)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(bio));
            if (string.IsNullOrWhiteSpace(profileImage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(profileImage));
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(lastName));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));

            Id = id;
            Bio = bio;
            ProfileImage = profileImage;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Active = active;
            Deleted = deleted;
        }
    }
}
