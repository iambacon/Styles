namespace IAmBacon.ViewModels.Post
{
    /// <summary>
    ///     The category count view model.
    ///     The category name and number of posts.
    /// </summary>
    public class CategoryCountViewModel
    {
        /// <summary>
        ///     Gets or sets the category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the number of posts for the category.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the percent.
        /// </summary>
        public double Percent { get; set; }

        /// <summary>
        /// Gets or sets the percent display text.
        /// </summary>
        /// <value>
        /// The percent display text.
        /// </value>
        public string PercentDisplayText
        {
            get { return string.Format("{0}%", (int)(this.Percent * 100)); }
        }

        /// <summary>
        ///     Gets or sets the category URL.
        /// </summary>
        public string Url { get; set; }
    }
}