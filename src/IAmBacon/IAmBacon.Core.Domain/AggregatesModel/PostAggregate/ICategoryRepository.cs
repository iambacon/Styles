using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category Add(Category entity);

        Task<Category> GetAsync(int categoryId);
    }
}
