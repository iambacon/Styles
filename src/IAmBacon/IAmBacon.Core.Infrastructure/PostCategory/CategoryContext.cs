using System.Threading.Tasks;
using IAmBacon.Core.Domain.Interfaces;
using IAmBacon.Core.Domain.PostCategory;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Infrastructure.PostCategory
{
    public class CategoryContext : DbContext, IUnitOfWork
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public async Task CommitAsync()
        {
            int result = await base.SaveChangesAsync();
        }
    }
}
