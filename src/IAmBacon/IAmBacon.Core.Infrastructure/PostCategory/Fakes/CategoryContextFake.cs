using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Infrastructure.PostCategory.Fakes
{
    public class CategoryContextFake : IUnitOfWork
    {
        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
