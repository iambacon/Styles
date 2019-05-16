using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmBacon.Core.Application.PostCategory.Queries.Fakes
{
    public class CategoryQueriesFake : ICategoryQueries
    {
        private readonly HashSet<Category> _data;

        public CategoryQueriesFake()
        {
            _data = new HashSet<Category>();
        }

        public void Add(Category entity)
        {
            _data.Add(entity);
        }

        public Task<Category> GetAsync(int id)
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

        public Task<IReadOnlyCollection<Category>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
