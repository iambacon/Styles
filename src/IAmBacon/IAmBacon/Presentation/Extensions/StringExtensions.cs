﻿namespace IAmBacon.Presentation.Extensions
{
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// String extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the first paragraph of html text.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>Returns the paragraph if successful, else returns the whole string.</returns>
        public static IHtmlString GetFirstParagraph(this string htmlText)
        {
            var match = MatchHtmlText(htmlText);

            return match.Success ? new HtmlString(match.Groups[1].Value.Trim()) : new HtmlString(htmlText);
        }

        /// <summary>
        /// Gets the first paragraph of html text.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>Returns the paragraph if successful, else returns the whole string.</returns>
        public static string GetFirstParagraph(this IHtmlString htmlText)
        {
            var match = MatchHtmlText(htmlText.ToString());

            return match.Success ? match.Groups[1].Value.Trim() : htmlText.ToString();
        }

        /// <summary>
        /// Truncates string at word by the specified length.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <returns>The truncated string.</returns>
        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
            {
                return input;
            }

            var nextSpaceIndex = input.LastIndexOf(" ", length, System.StringComparison.Ordinal);
            var truncatedLength = nextSpaceIndex > 0 ? nextSpaceIndex : length;
            var result = string.Format("{0}...", input.Substring(0, truncatedLength));

            return result;
        }

        /// <summary>
        /// Tries to match the first paragraph of HTML text.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>The <see cref="Match"/>.</returns>
        private static Match MatchHtmlText(string htmlText)
        {
            var match = Regex.Match(htmlText, @"<p>\s*(.+?)\s*</p>");
            return match;
        }
    }
}