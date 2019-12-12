using System;
namespace IAmBacon.Core.Application.PostTag.Commands
{
    public class UpdateTagCommand
    {
        public UpdateTagCommand(int id, string name, bool active, bool deleted)
        {
            Id = id;
            Active = active;
            Deleted = deleted;
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }

        public int Id { get; }
        public bool Active { get; }
        public bool Deleted { get; }
        public string Name { get; }
    }
}
