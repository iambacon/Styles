using System;
using System.Threading.Tasks;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

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

        public void Update(Tag entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Tag> GetAsync(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            return tag;
        }
    }
}
