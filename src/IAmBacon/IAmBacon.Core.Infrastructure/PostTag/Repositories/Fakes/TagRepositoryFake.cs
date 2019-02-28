using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Infrastructure.Base;
using IAmBacon.Core.Infrastructure.PostTag.Fakes;

namespace IAmBacon.Core.Infrastructure.PostTag.Repositories.Fakes
{
    public class TagRepositoryFake : RepositoryBaseFake<Tag>, ITagRepository
    {
        private readonly TagContextFake _context;

        public TagRepositoryFake(TagContextFake context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Tag Add(Tag entity)
        {
            Data.Add(entity);

            return entity;
        }
    }
}
