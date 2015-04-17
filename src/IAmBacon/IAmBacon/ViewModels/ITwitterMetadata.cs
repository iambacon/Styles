namespace IAmBacon.ViewModels
{
    /// <summary>
    /// Interface for Twitter metadata.
    /// </summary>
    public interface ITwitterMetadata
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has a twitter image.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.
        /// </value>
        bool HasImage { get; set; }

        /// <summary>
        /// Gets or sets the site.
        /// The Twitter @username the card should be attributed to.
        /// </summary>
        string Site { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// The canonical URL of your object that will be used as its permanent ID in the graph.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets the description that concisely summarizes the content of the page.
        /// Description text will be truncated at the word to 200 characters.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the image URL to a unique image representing the content of the page.
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Gets or sets the page title.
        /// Title truncated at 70 characters.
        /// </summary>
        string MetaTitle { get; set; }
    }
}
