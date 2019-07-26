using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAmBacon.Core.Application.User.Queries
{
    public interface IUserQueries
    {
        Task<User> GetAsync(int id);

        Task<IReadOnlyCollection<User>> GetAllAsync();
    }
}
