using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAmBacon.Domain.Commands
{
    public interface ICommand<TEntity> where TEntity : class
    {
        ICommandResult Execute(TEntity command);
    }
}
