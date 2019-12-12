using System.Threading.Tasks;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag Add(Tag entity);

        void Update(Tag entity);

        Task<Tag> GetAsync(int tagId);
    }
}
