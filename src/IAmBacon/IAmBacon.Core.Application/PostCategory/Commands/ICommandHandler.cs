using System.Threading.Tasks;

namespace IAmBacon.Core.Application.PostCategory.Commands
{
    internal interface ICommandHandler<in TCommand> where TCommand : class
    {
        Task HandleAsync(TCommand command);
    }
}
