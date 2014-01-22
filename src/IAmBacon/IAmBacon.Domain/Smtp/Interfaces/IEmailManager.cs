namespace IAmBacon.Domain.Smtp.Interfaces
{
    using System.Net.Mail;

    using IAmBacon.Model.Common;

    /// <summary>
    /// The email manager interface.
    /// </summary>
    public interface IEmailManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">The recipient.</param>
        /// <param name="from">The sender.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        /// <returns></returns>
        IResult SendEmail(MailAddress to, MailAddress from, string subject, string body, bool isHtml);

        /// <summary>
        /// Sends the email from the system email address.
        /// </summary>
        /// <param name="to">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        /// <returns></returns>
        IResult SendEmail(MailAddress to, string subject, string body, bool isHtml);

        /// <summary>
        /// Sends the new comment email.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        /// <returns></returns>
        IResult SendNewCommentEmail(string subject, string body, bool isHtml);

        #endregion
    }
}