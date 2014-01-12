namespace IAmBacon.Data.Infrastructure
{
    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork
    {
        #region Public Methods and Operators

        /// <summary>
        /// The commit.
        /// </summary>
        void Commit();

        #endregion
    }
}