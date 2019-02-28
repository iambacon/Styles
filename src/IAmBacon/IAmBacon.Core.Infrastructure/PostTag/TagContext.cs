using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Infrastructure.PostTag
{
    public class TagContext : DbContext, IUnitOfWork
    {
        public TagContext(DbContextOptions<TagContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }

        public async Task CommitAsync()
        {
            int result = await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
        }
    }
}
