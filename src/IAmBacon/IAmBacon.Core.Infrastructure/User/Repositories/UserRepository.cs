using System;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;
using IAmBacon.Core.Domain.Base;

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
    }
}
