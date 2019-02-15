using System;

namespace IAmBacon.Core.Application.PostCategory.Commands
{
    public class UpdateCategoryCommand
    {
        public UpdateCategoryCommand(int id, string name)
        {
            Id = id;
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }

        public int Id { get; }

        public string Name { get; }
    }
}
