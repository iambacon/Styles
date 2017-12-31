using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Infrastructure.Base;

namespace IAmBacon.Core.Infrastructure.PostCategory.Repositories.Fakes
{
    public class CategoryRepositoryFake : RepositoryBaseFake<Category>, ICategoryRepository
    {
        public CategoryRepositoryFake()
        {
        }

        public IUnitOfWork UnitOfWork => throw new System.NotImplementedException();

        public Category Add(Category entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
