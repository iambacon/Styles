using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Infrastructure.Post
{
    public class PostContext : DbContext, IUnitOfWork
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {
        }

        public DbSet<Domain.AggregatesModel.PostAggregate.Post> Posts { get; set; }

        public async Task CommitAsync()
        {
            int result = await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
        }
    }
}
