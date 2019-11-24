using System;
using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;
using IAmBacon.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Infrastructure.User.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Domain.AggregatesModel.UserAggregate.User Add(Domain.AggregatesModel.UserAggregate.User entity)
        {
            return _context.Users.Add(entity).Entity;
        }

        public void Update(Domain.AggregatesModel.UserAggregate.User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Domain.AggregatesModel.UserAggregate.User> GetAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }
    }
}
