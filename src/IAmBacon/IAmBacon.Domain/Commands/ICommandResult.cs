using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAmBacon.Domain.Commands
{
    public interface ICommandResult
    {
        bool Success { get; }
    }
}
