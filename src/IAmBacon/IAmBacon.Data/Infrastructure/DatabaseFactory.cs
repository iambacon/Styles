
namespace IAmBacon.Data.Infrastructure
{
    using IAmBacon.Data.Context;

    /// <summary>
    /// The database factory.
    /// </summary>
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        #region Fields

        /// <summary>
        /// The context.
        /// </summary>
        private BaconContext context;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="BaconContext"/>.
        /// </returns>
        public BaconContext Get()
        {
            return this.context ?? (this.context = new BaconContext());
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose core.
        /// </summary>
        protected override void DisposeCore()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }

        #endregion
    }
}