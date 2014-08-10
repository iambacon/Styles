using System.Collections.Generic;
using System.Linq;

namespace IAmBacon.ViewModels.Home
{
    /// <summary>
    /// View model for home page.
    /// </summary>
    public class HomeViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the blog posts.
        /// </summary>
        /// <value>
        /// The blog posts.
        /// </value>
        public IEnumerable<PostThumbViewModel> BlogPosts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show blog posts].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show blog posts]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowBlogPosts {
            get
            {
                return this.BlogPosts != null && this.BlogPosts.Any();
            }
        }
    }
}