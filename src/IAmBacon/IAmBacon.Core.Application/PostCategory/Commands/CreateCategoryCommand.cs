using System;

namespace IAmBacon.Core.Application.PostCategory.Commands
{
    public class CreateCategoryCommand
    {
        public string Name { get; }

        public CreateCategoryCommand(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }
    }
}
