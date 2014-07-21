namespace IAmBacon.ViewModels.Shared
{
    /// <summary>
    /// Popular post view model.
    /// Used in the site footer.
    /// </summary>
    public class PopularPostViewModel
    {
        /// <summary>
        /// Gets or sets the seo title.
        /// </summary>
        public string SeoTitle { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }
    }
}