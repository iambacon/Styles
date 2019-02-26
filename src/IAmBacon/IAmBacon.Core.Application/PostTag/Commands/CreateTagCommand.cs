using System;

namespace IAmBacon.Core.Application.PostTag.Commands
{
    public class CreateTagCommand
    {
        public CreateTagCommand(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }

        public string Name { get; }
    }
}
