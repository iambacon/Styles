namespace IAmBacon.Domain.Smtp
{
    using System;
    using System.Net.Mail;
    using System.Text;

    using IAmBacon.Domain.Services.Interfaces;
    using IAmBacon.Domain.Smtp.Interfaces;
    using IAmBacon.Model.Common;
    using IAmBacon.Model.Entities;

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
        ///     Sends the email.
        /// </summary>
        /// <param name="to">The recipient.</param>
        /// <param name="from">The sender.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public IResult SendEmail(MailAddress to, MailAddress from, string subject, string body)
        {
            var mailSent = true;
            var client = new SmtpClient();
            var message = new MailMessage(from, to) { Subject = subject, Body = body, BodyEncoding = Encoding.UTF8 };

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
        ///     Sends the email from the system email address.
        /// </summary>
        /// <param name="to">The recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public IResult SendEmail(MailAddress to, string subject, string body)
        {
            return this.SendEmail(to, SystemMailAddress, subject, body);
        }

        /// <summary>
        ///     Sends the new comment email.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IResult SendNewCommentEmail(Comment comment)
        {
            Post post = this.postService.Get(comment.PostId);
            string subject = "New comment";

            if (post != null)
            {
                subject = string.Format("New comment for {0}", post.Title);
            }

            var body = new StringBuilder();
            body.AppendLine(comment.Name);
            body.AppendLine(comment.Url);
            body.AppendLine(comment.Content);

            return this.SendEmail(CommentMailAddress, subject, body.ToString());
        }

        #endregion
    }
}