namespace IAmBacon.ViewModels.Shared
{
    using IAmBacon.Presentation.Enumerations;

    /// <summary>
    /// View model for a pagination link.
    /// </summary>
    public class PaginationItemViewModel
    {
        /// <summary>
        /// Gets or sets the href URL.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current page.
        /// </summary>
        public bool IsCurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the pagination text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the type of pagination item.
        /// </summary>
        public PaginationItemType Type { get; set; }
    }
}
