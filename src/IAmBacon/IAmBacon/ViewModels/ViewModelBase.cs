namespace IAmBacon.ViewModels
{
    /// <summary>
    /// The base view model class.
    /// </summary>
    public abstract class ViewModelBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle { get; set; }

        #endregion
    }
}