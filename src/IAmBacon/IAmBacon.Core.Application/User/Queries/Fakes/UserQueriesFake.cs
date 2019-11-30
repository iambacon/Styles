using System.Collections.Generic;
using System.Linq;
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

        public Task<User> GetAsync(int id)
        {
            try
            {
                var entity = _data.First(x => x.Id == id);
                return Task.FromResult(entity);
            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        public Task<User> GetAsync(string email)
        {
            try
            {
                var entity = _data.First(x => x.Email == email);
                return Task.FromResult(entity);
            }
            catch
            {
                throw new KeyNotFoundException();
            }
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
