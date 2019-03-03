using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Infrastructure.PostTag.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TagContext _context;

        public TagRepository(TagContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Tag Add(Tag entity)
        {
            return _context.Tags.Add(entity).Entity;
        }
    }
}
