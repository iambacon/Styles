namespace IAmBacon.Domain.Smtp
{
    using System;
    using System.Net.Mail;
    using System.Text;

    using IAmBacon.Domain.Services.Interfaces;
    using IAmBacon.Domain.Smtp.Interfaces;
    using IAmBacon.Model.Common;

    /// <summary>
    ///     Email manager. Handles SMTP messages.
    /// </summary>
    public class EmailManager : IEmailManager
    {
        #region Fields

        /// <summary>
        ///     The post service.
        /// </summary>
        private readonly IPostService postService;

        #endregion

        #region Constructors and Destructors

        public EmailManager(IPostService postService)
        {
            this.postService = postService;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the comment mail address.
        /// </summary>
        /// <value>
        ///     The comment mail address.
        /// </value>
        private static MailAddress CommentMailAddress
        {
            get
            {
                // TODO: Should be web config or db.
                return new MailAddress("comments@iambacon.co.uk");
            }
        }

        /// <summary>
        ///     Gets the system mail address.
        /// </summary>
        /// <value>
        ///     The system mail address.
        /// </value>
        private static MailAddress SystemMailAddress
        {
            get
            {
                return new MailAddress("email@iambacon.co.uk");
            }
        }

        #endregion

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
        public IResult SendEmail(MailAddress to, MailAddress from, string subject, string body, bool isHtml)
        {
            var mailSent = true;
            var client = new SmtpClient();
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = isHtml
            };

            try
            {
                client.Send(message);
            }
            catch (Exception)
            {
                mailSent = false;
            }
            finally
            {
                message.Dispose();
            }

            return new Result(mailSent);
        }

        /// <summary>
        /// Sends the email from the system email address.
        /// </summary>
        /// <param name="to">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        /// <returns></returns>
        public IResult SendEmail(MailAddress to, string subject, string body, bool isHtml)
        {
            return this.SendEmail(to, SystemMailAddress, subject, body, isHtml);
        }

        /// <summary>
        /// Sends the new comment email.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IResult SendNewCommentEmail(string subject, string body, bool isHtml)
        {
            return this.SendEmail(CommentMailAddress, subject, body, isHtml);
        }

        #endregion
    }
}