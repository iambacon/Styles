namespace IAmBacon.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The comment model.
    /// </summary>
    public class CommentModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Display(Name = "Comment")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date time formatted for time element yyyy-MM-ddTH:mm:ss
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public string DateTime { get; set; } 

        /// <summary>
        /// Gets or sets the display create date.
        /// </summary>
        public string DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the gravitar url.
        /// </summary>
        public string GravitarUrl { get; set; }

        /// <summary>
        /// Gets or sets the author of the comments name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [Display(Name = "Website")]
        public string Url { get; set; }
    }
}