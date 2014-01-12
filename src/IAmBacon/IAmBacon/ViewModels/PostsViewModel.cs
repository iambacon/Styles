using System.Collections.Generic;

namespace IAmBacon.ViewModels
{
    /// <summary>
    /// The posts view model. Used for tag and category results.
    /// </summary>
    public class PostsViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// The posts.
        /// </summary>
        public IEnumerable<PostViewModel> Posts;
    }
}