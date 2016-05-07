using System.Collections.Generic;
using System.Linq;
using IAmBacon.ViewModels.Post;

namespace IAmBacon.ViewModels
{
    using Presentation.Extensions;
    using Shared;

    using PagedList;

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
        /// Gets a value indicating whether [display categories].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display categories]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayCategories
        {
            get
            {
                return this.CategorySummaries != null && this.CategorySummaries.Any();
            }
        }

        /// <summary>
        /// Gets a value indicating whether [display pagination].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display pagination]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayPagination
        {
            get
            {
                return this.Posts.DisplayPagination();
            }
        }

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
                return this.Tags != null && this.Tags.Any();
            }
        }

        /// <summary>
        /// Gets or sets the pagination.
        /// </summary>
        public PaginationViewModel Pagination { get; set; }

        /// <summary>
        /// The paginated list of blog posts.
        /// </summary>
        public IPagedList<PostThumbViewModel> Posts { get; set; }

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