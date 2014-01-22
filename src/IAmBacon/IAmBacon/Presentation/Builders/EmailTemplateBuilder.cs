using System.Text;
using System.Web.Mvc;
using IAmBacon.Model.Entities;
using IAmBacon.Models;

namespace IAmBacon.Presentation.Builders
{
    /// <summary>
    /// Email template builder.  Creates messages for website emails.
    /// </summary>
    public static class EmailTemplateBuilder
    {
        /// <summary>
        /// Builds the new comment email.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="title">The title.</param>
        /// <param name="link">The link.</param>
        /// <param name="linkText">The link text.</param>
        /// <returns></returns>
        public static EmailTemplateModel NewCommentEmail(Comment comment, string title, string link, string linkText)
        {
            var subject = string.Format("New comment for {0}", title);

            // If we've detected it as spam, mark the subject so it's obvious.
            if (!comment.Active)
            {
                subject = string.Format("[SPAM] {0}", subject);
            }

            var commentAnchor = new TagBuilder("a");
            commentAnchor.SetInnerText(linkText);
            commentAnchor.Attributes["href"] = link;

            var body = new StringBuilder();
            body.AppendLine(comment.Name);
            body.AppendLine("<br />");
            body.AppendLine(comment.Url);
            body.AppendLine("<br />");
            body.AppendLine(comment.Content);
            body.AppendLine("<br />");
            body.AppendLine(commentAnchor.ToString());
            
            var emailTemplate = new EmailTemplateModel
            {
                Body = body.ToString(),
                Subject = subject,
                IsHtml = true
            };

            return emailTemplate;
        }
    }
}