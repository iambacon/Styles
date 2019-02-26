using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag Add(Tag entity);
    }
}
