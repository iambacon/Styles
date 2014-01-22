using IAmBacon.Model.Entities.Interfaces;

namespace IAmBacon.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The comment.
    /// </summary>
    public class Comment : IComment
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [active].
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        [Required]
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the gravitar hash.
        /// </summary>
        [MaxLength(32)]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the author of the comments name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        [Required]
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [MaxLength(100)]
        public string Url { get; set; }

        #endregion
    }
}