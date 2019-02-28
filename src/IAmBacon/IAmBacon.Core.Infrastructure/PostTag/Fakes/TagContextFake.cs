using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Infrastructure.PostTag.Fakes
{
    public class TagContextFake : IUnitOfWork
    {
        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
