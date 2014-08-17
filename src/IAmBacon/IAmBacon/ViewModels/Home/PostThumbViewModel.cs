namespace IAmBacon.ViewModels.Home
{
    /// <summary>
    /// The post thum view model.
    /// A simple post view model used to advertise a post in a list.
    /// </summary>
    public class PostThumbViewModel
    {
        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the formated date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public string DateTime { get; set; }

        /// <summary>
        /// Gets or sets the display date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string DisplayDate { get; set; }

        /// <summary>
        /// Gets or sets the seo title.
        /// </summary>
        /// <value>
        /// The seo title.
        /// </value>
        public string SeoTitle { get; set; }

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