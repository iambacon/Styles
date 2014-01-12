namespace IAmBacon.Data.Repositories
{
    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Model.Entities;

    /// <summary>
    /// The sql posts repository.
    /// </summary>
    public class SqlPostsRepository : SqlRepositoryBase<Post>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPostsRepository"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public SqlPostsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #endregion
    }
}