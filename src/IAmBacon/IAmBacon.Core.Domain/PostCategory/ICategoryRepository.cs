using IAmBacon.Core.Domain.Interfaces;

namespace IAmBacon.Core.Domain.PostCategory
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Add(Category entity);
    }
}
