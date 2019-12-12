using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    public interface IPostRepository : IRepository<Post>
    {
        Post Add(Post entity);

        PostTag Add(PostTag entity);

        void Update(Post entity);

        Task<Post> GetAsync(int postId);
    }
}
