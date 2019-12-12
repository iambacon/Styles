using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Infrastructure.Post;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagEntityTypeConfiguration());
        }
    }
}
