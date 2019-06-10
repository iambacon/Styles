using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        User Add(User entity);
    }
}
