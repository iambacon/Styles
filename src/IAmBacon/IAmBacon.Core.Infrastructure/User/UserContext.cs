using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Infrastructure.User
{
    public class UserContext : DbContext, IUnitOfWork
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<Domain.AggregatesModel.UserAggregate.User> Users { get; set; }

        public async Task CommitAsync()
        {
            int result = await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
