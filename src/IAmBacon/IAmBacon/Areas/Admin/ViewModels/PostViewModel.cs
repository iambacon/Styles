namespace IAmBacon.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;

    using IAmBacon.Model.Entities;

    public class PostViewModel
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        /// <value>
        ///     The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the category name.
        /// </summary>
        /// <value>
        ///     The category.
        /// </value>
        public string CategoryName { get; set; }

        /// <summary>
        ///     Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///     Gets or sets the date modified.
        /// </summary>
        /// <value>
        ///     The date modified.
        /// </value>
        public DateTime DateModified { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///     Gets or sets the tags.
        /// </summary>
        public virtual IList<Tag> Tags { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}