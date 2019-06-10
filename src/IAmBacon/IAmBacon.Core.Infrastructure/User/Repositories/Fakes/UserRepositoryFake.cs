using System;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Infrastructure.Base;
using IAmBacon.Core.Infrastructure.User.Fakes;

namespace IAmBacon.Core.Infrastructure.User.Repositories.Fakes
{
    public class UserRepositoryFake : RepositoryBaseFake<Domain.AggregatesModel.UserAggregate.User>, IUserRepository
    {
        private readonly UserContextFake _context;

        public UserRepositoryFake(UserContextFake context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Domain.AggregatesModel.UserAggregate.User Add(Domain.AggregatesModel.UserAggregate.User entity)
        {
            Data.Add(entity);

            return entity;
        }
    }
}
