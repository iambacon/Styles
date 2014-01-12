namespace IAmBacon.Data.Infrastructure
{
    using IAmBacon.Data.Context;

    /// <summary>
    /// The unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        /// <summary>
        /// The database factory.
        /// </summary>
        private readonly IDatabaseFactory databaseFactory;

        /// <summary>
        /// The context.
        /// </summary>
        private BaconContext context;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected BaconContext Context
        {
            get
            {
                return this.context ?? (this.context = this.databaseFactory.Get());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The commit.
        /// </summary>
        public void Commit()
        {
            this.Context.Commit();
        }

        #endregion
    }
}