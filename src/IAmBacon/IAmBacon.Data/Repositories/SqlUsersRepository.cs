namespace IAmBacon.Data.Repositories
{
    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Model.Entities;

    /// <summary>
    /// The sql users repository.
    /// </summary>
    public class SqlUsersRepository : SqlRepositoryBase<User>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlUsersRepository"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public SqlUsersRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #endregion
    }
}