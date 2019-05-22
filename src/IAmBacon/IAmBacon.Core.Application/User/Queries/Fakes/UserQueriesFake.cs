using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAmBacon.Core.Application.User.Queries.Fakes
{
    public class UserQueriesFake : IUserQueries
    {
        private readonly HashSet<User> _data;

        public UserQueriesFake()
        {
            _data = new HashSet<User>();
        }

        public Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return Task.FromResult(_data as IReadOnlyCollection<User>);
        }

        public void Add(User entity)
        {
            _data.Add(entity);
        }
    }
}
