using System.Collections.Generic;
using System.Linq;
using IAmBacon.ViewModels.Post;

namespace IAmBacon.ViewModels
{
    /// <summary>
    /// The posts view model. Used for tag and category results.
    /// </summary>
    public class PostsViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the category summaries.
        /// </summary>
        /// <value>
        /// The category summuries.
        /// </value>
        public IEnumerable<CategoryCountViewModel> CategorySummaries { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display categories].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display categories]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayCategories { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display tags].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display tags]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayTags
        {
            get
            {
                return Tags != null && Tags.Any();
            }
        }

        /// <summary>
        /// The posts.
        /// </summary>
        public IEnumerable<PostViewModel> Posts { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}