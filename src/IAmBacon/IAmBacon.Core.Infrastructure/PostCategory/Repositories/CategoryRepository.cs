using System;
using IAmBacon.Core.Domain.Interfaces;
using IAmBacon.Core.Domain.PostCategory;

namespace IAmBacon.Core.Infrastructure.PostCategory.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private CategoryContext _context;

        public CategoryRepository(CategoryContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}