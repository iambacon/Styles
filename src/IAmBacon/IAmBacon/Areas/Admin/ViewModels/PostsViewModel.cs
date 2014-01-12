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

        #endregion
    }
}