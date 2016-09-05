namespace IAmBacon.ViewModels.Shared
{
    using System.Collections.Generic;
    using System.Web;

    using Post;

    /// <summary>
    /// The post thumb view model.
    /// A simple post view model used to advertise a post in a list.
    /// </summary>
    public class PostThumbViewModel
    {
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public IHtmlString Content { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public string DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the formatted date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public string DateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display category].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display category]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayCategory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display content].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display content]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayContent { get; set; }

        /// <summary>
        /// Gets or sets the display date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string DisplayDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to [display tags].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display tags]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayTags { get; set; }

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
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        /// <value>
        /// The thumbnail.
        /// </value>
        public string Thumbnail { get; set; }
    }
}