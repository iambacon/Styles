using System.Threading.Tasks;

namespace IAmBacon.Core.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
