using System;
using System.Linq;
using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Infrastructure.Base;
using IAmBacon.Core.Infrastructure.PostCategory.Fakes;

namespace IAmBacon.Core.Infrastructure.PostCategory.Repositories.Fakes
{
    public class CategoryRepositoryFake : RepositoryBaseFake<Category>, ICategoryRepository
    {
        private readonly CategoryContextFake _context;

        public CategoryRepositoryFake(CategoryContextFake context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Category Add(Category entity)
        {
            Data.Add(entity);

            return entity;
        }

        public void Update(Category entity)
        {
            Data.RemoveWhere(x => x.Id == entity.Id);
            Add(entity);
        }

        public Task<Category> GetAsync(int categoryId)
        {
            var entity = Data.FirstOrDefault(x => x.Id == categoryId);
            return Task.FromResult(entity);
        }
    }
}
