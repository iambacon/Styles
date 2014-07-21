using System.Collections.Generic;

namespace IAmBacon.ViewModels.Shared
{
    /// <summary>
    /// The footer view model.
    /// </summary>
    public class FooterViewModel
    {
        /// <summary>
        /// Gets or sets the recent posts.
        /// </summary>
        /// <value>
        /// The recent posts.
        /// </value>
        public IEnumerable<RecentPostViewModel> RecentPosts { get; set; }

        /// <summary>
        /// Gets or sets the popular posts.
        /// </summary>
        /// <value>
        /// The popular posts.
        /// </value>
        public IEnumerable<PopularPostViewModel> PopularPosts { get; set; }
    }
}