using System;
using System.Linq;
using System.Threading.Tasks;
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

        public void Update(Domain.AggregatesModel.UserAggregate.User entity)
        {
            Data.RemoveWhere(x => x.Id == entity.Id);
            Add(entity);
        }

        public Task<Domain.AggregatesModel.UserAggregate.User> GetAsync(int userId)
        {
            var entity = Data.FirstOrDefault(x => x.Id == userId);
            return Task.FromResult(entity);
        }
    }
}
