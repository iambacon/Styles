using System;
using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Infrastructure.Post.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PostContext _context;

        public PostRepository(PostContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Domain.AggregatesModel.PostAggregate.Post Add(Domain.AggregatesModel.PostAggregate.Post entity)
        {
            return _context.Posts.Add(entity).Entity;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Update(Domain.AggregatesModel.PostAggregate.Post entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Domain.AggregatesModel.PostAggregate.Post> GetAsync(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            return post;
        }
    }
}
