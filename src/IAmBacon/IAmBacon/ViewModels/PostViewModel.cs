using System.Linq;
using IAmBacon.ViewModels.Post;

namespace IAmBacon.ViewModels
{
    using System.Collections.Generic;
    using System.Web;
    using Models;

    /// <summary>
    /// The post view model.
    /// </summary>
    public class PostViewModel : ViewModelBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public IEnumerable<CommentModel> Comments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [comments active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [comments active]; otherwise, <c>false</c>.
        /// </value>
        public bool CommentsActive { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public IHtmlString Content { get; set; }

        /// <summary>
        /// Gets or sets the date time formatted for time element yyyy-MM-ddTH:mm:ss
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public string DateTime { get; set; }

        /// <summary>
        /// Gets or sets the display create date.
        /// </summary>
        public string DateCreated { get; set; }

        /// <summary>
        /// Gets a value indicating whether [display tags].
        /// </summary>
        public bool DisplayTags
        {
            get { return Tags != null && Tags.Any(); }
        }

        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the new comment.
        /// </summary>
        /// <value>
        /// The new comment.
        /// </value>
        public CommentModel NewComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [no CSS].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no CSS]; otherwise, <c>false</c>.
        /// </value>
        public bool NoCss { get; set; }

        /// <summary>
        /// Gets or sets the seo title.
        /// </summary>
        /// <value>
        /// The seo title.
        /// </value>
        public string SeoTitle { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public IEnumerable<TagViewModel> Tags { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}