using IAmBacon.ViewModels.Shared;

namespace IAmBacon.ViewModels
{
    /// <summary>
    /// The base view model class.
    /// </summary>
    public abstract class ViewModelBase
    {
        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public FooterViewModel Footer { get; set; }
    }
}