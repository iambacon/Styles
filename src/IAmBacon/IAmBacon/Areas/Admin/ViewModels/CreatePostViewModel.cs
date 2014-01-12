// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePostViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The create post view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IAmBacon.Areas.Admin.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using IAmBacon.Models;

    /// <summary>
    /// The create post view model.
    /// </summary>
    public class CreatePostViewModel
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
        /// <value>
        /// The author id.
        /// </value>
        public int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        public IEnumerable<SelectListItem> Authors { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public IEnumerable<SelectListItem> Categories { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [Required]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the markdown.
        /// </summary>
        [Required]
        public string Markdown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [no CSS].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no CSS]; otherwise, <c>false</c>.
        /// </value>
        public bool NoCss { get; set; }

        /// <summary>
        /// Gets or sets the tag id.
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public IEnumerable<CheckboxItem> Tags { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        #endregion
    }
}