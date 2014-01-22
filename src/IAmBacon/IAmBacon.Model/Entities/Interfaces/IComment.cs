namespace IAmBacon.Model.Entities.Interfaces
{
    /// <summary>
    /// The comment interface.
    /// </summary>
    public interface IComment : IEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether [active].
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets the gravitar hash.
        /// </summary>
        string Hash { get; set; }

        /// <summary>
        /// Gets or sets the author of the comments name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        int PostId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        string Url { get; set; }
    }
}