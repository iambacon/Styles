using System.Threading.Tasks;

namespace IAmBacon.Core.Application.Post.Queries
{
    public interface IPostQueries
    {
        Task<Post> GetAsync(int id);
    }
}
