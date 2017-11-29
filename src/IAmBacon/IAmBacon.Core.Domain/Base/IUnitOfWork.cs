using System.Threading.Tasks;

namespace IAmBacon.Core.Domain.Base
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
