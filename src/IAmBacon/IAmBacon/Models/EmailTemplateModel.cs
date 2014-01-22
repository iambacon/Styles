namespace IAmBacon.Models
{
    /// <summary>
    /// The email template model.
    /// </summary>
    public class EmailTemplateModel
    {
        /// <summary>
        /// Gets or sets the email body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is HTML].
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the to address.
        /// </summary>
        public string To { get; set; }
    }
}