namespace IAmBacon.Areas.Admin.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The comments view model.
    /// </summary>
    public class CommentsViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public IEnumerable<CommentViewModel> Comments { get; set; }

        #endregion
    }
}