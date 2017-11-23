using System;
using IAmBacon.Core.Domain.Interfaces;
using IAmBacon.Core.Domain.PostCategory;

namespace IAmBacon.Core.Infrastructure.PostCategory.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _context;

        public CategoryRepository(CategoryContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}