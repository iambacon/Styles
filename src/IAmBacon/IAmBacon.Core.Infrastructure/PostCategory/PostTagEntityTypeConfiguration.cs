using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAmBacon.Core.Infrastructure.PostCategory
{
    public class PostTagEntityTypeConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.PostAggregate.PostTag>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.PostAggregate.PostTag> builder)
        {
            builder.HasKey(o => new { o.PostId, o.TagId });
            builder.HasOne(o => o.Post).WithMany(p => p.PostTags).HasForeignKey(o => o.PostId);
            builder.HasOne(o => o.Tag).WithMany(t => t.Posts).HasForeignKey(o => o.TagId);
        }
    }
}
