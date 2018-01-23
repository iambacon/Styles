using System;
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
    }
}
