namespace IAmBacon.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Interfaces;

    /// <summary>
    /// The tag.
    /// </summary>
    public class Tag : ITag
    {
        #region Public Properties

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
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        public virtual IList<Post> Posts { get; set; }

        /// <summary>
        /// Gets or sets the seo version of the name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string SeoName { get; set; }

        #endregion
    }
}