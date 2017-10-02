using System;

namespace IAmBacon.Core.Application.PostCategory.Commands
{
    public class CreateCategoryCommand
    {
        public string Name { get; }

        public CreateCategoryCommand(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name)? name : throw new ArgumentNullException("{name} cannot be null or empty", nameof(name));
        }
    }
}
