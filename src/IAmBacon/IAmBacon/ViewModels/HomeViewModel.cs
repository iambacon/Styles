using IAmBacon.ViewModels.Shared;

namespace IAmBacon.ViewModels
{
    /// <summary>
    /// View model for home page.
    /// </summary>
    public class HomeViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public FooterViewModel Footer { get; set; }
    }
}