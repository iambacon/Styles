namespace IAmBacon.Presentation.Extensions
{
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// String extension methods.
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the first paragraph of html text.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>Returns the paragraph if successful, else returns the whole string.</returns>
        public static IHtmlString GetFirstParagraph(this string htmlText)
        {
            var match = Regex.Match(htmlText, @"<p>\s*(.+?)\s*</p>");

            return match.Success ? new HtmlString(match.Groups[1].Value.Trim()) : new HtmlString(htmlText);
        }

        #endregion
    }
}