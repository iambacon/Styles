using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmBacon.Core.Application.PostTag.Queries.Fakes
{
    public class TagQueriesFake : ITagQueries
    {
        private readonly HashSet<Tag> _data;

        public TagQueriesFake()
        {
            _data = new HashSet<Tag>();
        }

        public void Add(Tag entity)
        {
            _data.Add(entity);
        }

        public Task<Tag> GetAsync(int id)
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

        public Task<IReadOnlyCollection<Tag>> GetAllAsync()
        {
            return Task.FromResult(_data as IReadOnlyCollection<Tag>);
        }

        public Task<IReadOnlyCollection<Tag>> GetTagsForPost(int postId)
        {
            throw new System.NotImplementedException();
        }
    }
}
