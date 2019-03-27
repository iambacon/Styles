using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Infrastructure.Post.Fakes
{
    public class PostContextFake : IUnitOfWork
    {
        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
