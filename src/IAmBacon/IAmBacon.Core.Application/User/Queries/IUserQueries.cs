using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAmBacon.Core.Application.User.Queries
{
    public interface IUserQueries
    {
        Task<IReadOnlyCollection<User>> GetAllAsync();
    }
}
