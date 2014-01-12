namespace IAmBacon.Data.Repositories
{
    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Model.Entities;

    /// <summary>
    /// The sql categories repository.
    /// </summary>
    public class SqlCategoriesRepository : SqlRepositoryBase<Category>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCategoriesRepository"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public SqlCategoriesRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #endregion
    }
}