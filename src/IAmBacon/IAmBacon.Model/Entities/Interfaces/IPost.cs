namespace IAmBacon.Model.Entities.Interfaces
{
    /// <summary>
    /// The Post interface.
    /// </summary>
    public interface IPost : IEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [active]; otherwise, <c>false</c>.
        /// </value>
        bool Active { get; set; }

        /// <summary>
        /// Gets or sets the author id.
        /// </summary>
        int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        int CategoryId { get; set; }
        
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets the markdown.
        /// </summary>
        string Markdown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [no CSS].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no CSS]; otherwise, <c>false</c>.
        /// </value>
        bool NoCss { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        string Image { get; set; }

        #endregion
    }
}