using System;
using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

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

        public void Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Category> GetAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return category;
        }
    }
}