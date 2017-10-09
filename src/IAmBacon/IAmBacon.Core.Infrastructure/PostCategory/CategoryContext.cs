using System.Threading.Tasks;
using IAmBacon.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Infrastructure.PostCategory
{
    public class CategoryContext : DbContext, IUnitOfWork
    {
        public async Task CommitAsync()
        {
            int result = await base.SaveChangesAsync();
        }
    }
}
