using IAmBacon.ViewModels.Post;

namespace IAmBacon.Areas.Admin.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The posts view model.
    /// </summary>
    public class PostsViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        public IEnumerable<PostViewModel> Posts { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public IEnumerable<CategoryCountViewModel> Categories { get; set; }

        #endregion
    }
}