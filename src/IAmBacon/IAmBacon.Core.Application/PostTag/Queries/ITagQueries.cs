using System.Collections.Generic;
using System.Threading.Tasks;
namespace IAmBacon.Core.Application.PostTag.Queries
{
    public interface ITagQueries
    {
        Task<Tag> GetAsync(int id);

        Task<IReadOnlyCollection<Tag>> GetAllAsync();
    }
}
