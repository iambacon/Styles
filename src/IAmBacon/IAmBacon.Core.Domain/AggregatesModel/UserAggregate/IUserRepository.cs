using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        User Add(User entity);

        void Update(User entity);

        Task<User> GetAsync(int userId);
    }
}
