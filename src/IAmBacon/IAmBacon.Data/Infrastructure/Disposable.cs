namespace IAmBacon.Data.Infrastructure
{
    using System;

    /// <summary>
    /// The disposable.
    /// </summary>
    public class Disposable : IDisposable
    {
        #region Fields

        /// <summary>
        /// The is disposed.
        /// </summary>
        private bool isDisposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="Disposable"/> class. 
        /// </summary>
        ~Disposable()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose core.
        /// </summary>
        protected virtual void DisposeCore()
        {
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.DisposeCore();
            }

            this.isDisposed = true;
        }

        #endregion
    }
}