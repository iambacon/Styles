namespace IAmBacon.Data.Infrastructure
{
    using System;

    using IAmBacon.Data.Context;

    /// <summary>
    /// The DatabaseFactory interface.
    /// </summary>
    public interface IDatabaseFactory : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="BaconContext"/>.
        /// </returns>
        BaconContext Get();

        #endregion
    }
}