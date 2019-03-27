using System;
using System.Linq;
using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Infrastructure.Base;
using IAmBacon.Core.Infrastructure.Post.Fakes;

namespace IAmBacon.Core.Infrastructure.Post.Repositories.Fakes
{
    public class PostRepositoryFake :RepositoryBaseFake<Domain.AggregatesModel.PostAggregate.Post>, IPostRepository
    {
        private readonly PostContextFake _context;

        public PostRepositoryFake(PostContextFake context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Domain.AggregatesModel.PostAggregate.Post Add(Domain.AggregatesModel.PostAggregate.Post entity)
        {
            Data.Add(entity);

            return entity;
        }

        public void Update(Domain.AggregatesModel.PostAggregate.Post entity)
        {
            Data.RemoveWhere(x => x.Id == entity.Id);
            Add(entity);
        }

        public Task<Domain.AggregatesModel.PostAggregate.Post> GetAsync(int postId)
        {
            var entity = Data.FirstOrDefault(x => x.Id == postId);
            return Task.FromResult(entity);
        }
    }
}
