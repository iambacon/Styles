using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Infrastructure.PostTag.Repositories
{
    public class TagRepository : ITagRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public Tag Add(Tag entity)
        {
            throw new NotImplementedException();
        }
    }
}
