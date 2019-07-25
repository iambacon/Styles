using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAmBacon.Core.Infrastructure.User
{
    public class UserEntityTypeConfiguration: IEntityTypeConfiguration<Domain.AggregatesModel.UserAggregate.User>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.UserAggregate.User> builder)
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
            builder.Property<string>("Email").HasField("_email").IsRequired().HasMaxLength(100);
            builder.Property<string>("FirstName").HasField("_firstName").IsRequired();
            builder.Property<string>("LastName").HasField("_lastName").IsRequired();
            builder.Property<string>("Bio").HasField("_bio").HasMaxLength(510).IsRequired();
            builder.Property<string>("ProfileImage").HasField("_profileImage").HasMaxLength(255).IsRequired();

            // Configure entity filters
            builder.HasQueryFilter(c => !c.Deleted);
        }
    }
}
