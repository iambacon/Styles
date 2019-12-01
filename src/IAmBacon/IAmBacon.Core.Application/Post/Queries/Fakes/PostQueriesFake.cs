using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmBacon.Core.Application.Post.Queries.Fakes
{
    public class PostQueriesFake : IPostQueries
    {
        private readonly HashSet<Post> _data;

        public PostQueriesFake()
        {
            _data = new HashSet<Post>();
        }

        public void Add(Post entity)
        {
            _data.Add(entity);
        }

        public Task<Post> GetAsync(int id)
        {
            try
            {
                var entity = _data.First(x => x.Id == id);
                return Task.FromResult(entity);
            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        public Task<IReadOnlyCollection<Post>> GetAllAsync()
        {
            return Task.FromResult(_data as IReadOnlyCollection<Post>);
        }
    }
}
