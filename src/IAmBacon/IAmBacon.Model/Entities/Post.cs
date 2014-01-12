namespace IAmBacon.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Interfaces;

    /// <summary>
    /// The post.
    /// </summary>
    public class Post : IPost
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [active]; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the author id.
        /// </summary>
        [ForeignKey("User")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public virtual IList<Comment> Comments { get; set; }

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
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [Column(TypeName = "varchar(MAX)")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the markdown.
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string Markdown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [no CSS].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no CSS]; otherwise, <c>false</c>.
        /// </value>
        public bool NoCss { get; set; }

        /// <summary>
        /// Gets or sets the seo version of the title.
        /// </summary>
        [Required]
        [MaxLength(510)]
        public string SeoTitle { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public virtual IList<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }
        
        #endregion
    }
}