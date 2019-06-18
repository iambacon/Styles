using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAmBacon.Core.Infrastructure.Post
{
    public class PostEntityTypeConfiguration: IEntityTypeConfiguration<Domain.AggregatesModel.PostAggregate.Post>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.PostAggregate.Post> builder)
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
            builder.Property(b => b.IsActive).HasColumnName("Active");
            builder.Property<int>("AuthorId").HasField("_authorId");
            builder.Property<int>("CategoryId").HasField("_categoryId");
            builder.Property<string>("Title").HasField("_title").IsRequired().HasMaxLength(255);
            builder.Property<string>("Content").HasField("_content").IsRequired().HasColumnType("varchar(MAX)");
            builder.Property<string>("Image").HasField("_image").HasColumnType("varchar(MAX)");
            builder.Property<string>("Markdown").HasField("_markdown").HasColumnType("varchar(MAX)");
            builder.Property<string>("SeoTitle").HasField("_seoTitle").HasMaxLength(510);
            builder.Property<bool>("NoCss").HasField("_noCss");
        }
    }
}
