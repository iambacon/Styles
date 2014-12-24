using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Gets a value indicating whether [show recent posts].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show recent posts]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRecentPosts
        {
            get
            {
                return this.RecentPosts != null && this.RecentPosts.Any();
            }
        }

        /// <summary>
        /// Gets a value indicating whether [show popular posts].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show popular posts]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowPopularPosts
        {
            get
            {
                return this.PopularPosts != null && this.PopularPosts.Any();
            }
        }
    }
}