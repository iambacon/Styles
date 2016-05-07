using System.Collections.Generic;
using System.Linq;

namespace IAmBacon.ViewModels.Home
{
    using IAmBacon.ViewModels.Shared;

    /// <summary>
    /// View model for home page.
    /// </summary>
    public class HomeViewModel : ViewModelBase, ITwitterMetadata
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
        public bool ShowBlogPosts
        {
            get
            {
                return this.BlogPosts != null && this.BlogPosts.Any();
            }
        }

        ///<inheritdoc />
        public bool HasImage { get; set; }

        ///<inheritdoc />
        public string Site { get; set; }

        ///<inheritdoc />
        public string Url { get; set; }

        ///<inheritdoc />
        public string Description { get; set; }

        ///<inheritdoc />
        public string Image { get; set; }

        ///<inheritdoc />
        public string MetaTitle { get; set; }
    }
}