namespace IAmBacon.Data.Repositories
{
    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Model.Entities;

    /// <summary>
    /// The sql tags repository.
    /// </summary>
    public class SqlTagsRepository : SqlRepositoryBase<Tag>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTagsRepository"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public SqlTagsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #endregion
    }
}