using System.Threading.Tasks;

namespace IAmBacon.Core.Application.Base
{
    internal interface ICommandHandler<in TCommand> where TCommand : class
    {
        Task HandleAsync(TCommand command);
    }
}
