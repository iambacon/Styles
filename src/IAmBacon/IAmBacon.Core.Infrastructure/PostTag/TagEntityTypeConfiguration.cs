using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAmBacon.Core.Infrastructure.PostTag
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            // May not need this but have added to be explicit
            builder.HasKey(o => o.Id);

            // This seems to be required to get properties to map but I would have thought there is a setting
            // to do this automatically rather than specifying each column and field
            //
            // I've added the required and max length properties as they were on the old class
            // as data annotations. I'm guessing I need them for when the db is created
            // Need to test this!!
            builder.Property<DateTime>("DateCreated").HasField("_dateCreated").IsRequired();
            builder.Property<DateTime>("DateModified").HasField("_dateModified").IsRequired();
            builder.Property<string>("Name").IsRequired().HasMaxLength(150);
            builder.Property<string>("SeoName").HasField("_seoName").HasMaxLength(255);
            builder.Property<bool>("Active").HasField("_active");

            // Configure entity filters
            builder.HasQueryFilter(c => !c.Deleted);
        }
    }
}
