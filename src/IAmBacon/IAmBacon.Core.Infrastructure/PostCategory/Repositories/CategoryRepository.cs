using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Infrastructure.PostCategory.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _context;

        public CategoryRepository(CategoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Category Add(Category entity)
        {
            return _context.Categories.Add(entity).Entity;
        }
    }
}