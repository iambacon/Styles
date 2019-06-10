using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Infrastructure.User.Fakes
{
    public class UserContextFake : IUnitOfWork
    {
        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
