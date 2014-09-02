using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using IAmBacon.Model.Entities.Interfaces;

namespace IAmBacon.Data.Context
{
    using System.Data.Entity;

    using Model.Entities;

    /// <summary>
    /// The bacon context.
    /// </summary>
    public class BaconContext : DbContext
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion

        public BaconContext()
        {
            // TODO: Remove this once it is working correctly on laptop
            Database.SetInitializer<BaconContext>(null);
        }

        #region Public Methods and Operators

        /// <summary>
        /// The commit.
        /// </summary>
        public virtual void Commit()
        {
            SaveChanges();
        }

        public override int SaveChanges()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;
            var objectStateEntries =
                context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                .Where(x =>
                    x.IsRelationship == false &&
                    x.Entity is IEntity);

            var currentTime = DateTime.Now;

            foreach (var objectStateEntry in objectStateEntries)
            {
                var entity = (IEntity)objectStateEntry.Entity;

                if (entity == null)
                {
                    continue;
                }

                if (objectStateEntry.State == EntityState.Added)
                {
                    entity.DateCreated = currentTime;
                }

                entity.DateModified = currentTime;
            }

            return base.SaveChanges();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on model creating override method.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Tags)
                .WithMany(x => x.Posts).Map(
                x =>
                {
                    x.ToTable("PostTags");
                    x.MapLeftKey("PostId");
                    x.MapRightKey("TagId");
                });
        }

        #endregion
    }
}