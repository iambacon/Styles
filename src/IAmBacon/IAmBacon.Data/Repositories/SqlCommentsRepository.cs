namespace IAmBacon.Data.Repositories
{
    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Model.Entities;

    /// <summary>
    /// The sql comments repository.
    /// </summary>
    public class SqlCommentsRepository : SqlRepositoryBase<Comment>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCommentsRepository"/> class.
        /// </summary>
        /// <param name="databaseFactory">The database factory.</param>
        public SqlCommentsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #endregion
    }
}