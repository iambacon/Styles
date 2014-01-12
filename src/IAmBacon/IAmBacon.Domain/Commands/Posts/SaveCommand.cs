using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAmBacon.Model.Entities;

namespace IAmBacon.Domain.Commands.Posts
{
    public class SaveCommand : ICommand<Post>
    {
        public ICommandResult Execute(Post command)
        {
            throw new NotImplementedException();
        }
    }
}
