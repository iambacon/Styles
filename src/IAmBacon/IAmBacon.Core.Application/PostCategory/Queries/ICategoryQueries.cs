using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAmBacon.Core.Application.PostCategory.Queries
{
    public interface ICategoryQueries
    {
        Task<Category> GetAsync(int id);

        Task<IReadOnlyCollection<Category>> GetAllAsync();
    }
}
