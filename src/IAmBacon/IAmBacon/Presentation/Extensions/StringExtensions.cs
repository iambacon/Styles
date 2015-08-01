namespace IAmBacon.Presentation.Extensions
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    using Microsoft.Ajax.Utilities;

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
            var match = MatchFirstParagraphTag(htmlText);

            return match.Success ? new HtmlString(match.Groups[1].Value.Trim()) : new HtmlString(htmlText);
        }

        /// <summary>
        /// Gets the first paragraph of html text.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>Returns the paragraph if successful, else returns the whole string.</returns>
        public static string GetFirstParagraph(this IHtmlString htmlText)
        {
            var match = MatchFirstParagraphTag(htmlText.ToString());

            return match.Success ? match.Groups[1].Value.Trim() : htmlText.ToString();
        }

        /// <summary>
        /// Gets the first paragraph of html text and image if one is available.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns><see cref="IHtmlString"/>. The summary text.</returns>
        public static IHtmlString GetSummary(this string htmlText)
        {
            string summary;
            string image;

            TryMatchParagraph(htmlText, out summary);

            if (TryMatchImage(summary, out image))
            {
                htmlText = htmlText.Replace(summary, "");

                string secondParagraph;
                TryMatchParagraph(htmlText, out secondParagraph);

                summary += secondParagraph;
            }

            return new HtmlString(summary);
        }

        /// <summary>
        /// Try to match paragraph tag in specified HTML text.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <param name="result">The result.</param>
        /// <returns><see cref="bool"/>. If match then true else false.</returns>
        private static bool TryMatchParagraph(string htmlText, out string result)
        {
            result = null;

            if (htmlText == null)
            {
                return false;
            }

            Match paragraphMatch = MatchFirstParagraphTag(htmlText);

            if (paragraphMatch.Success)
            {
                result = paragraphMatch.Groups[0].Value.Trim();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to match IMG tag in the specified HTML text.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <param name="result">The result.</param>
        /// <returns><see cref="bool"/>. If match then true else false.</returns>
        private static bool TryMatchImage(string htmlText, out string result)
        {
            result = null;

            if (htmlText == null)
            {
                return false;
            }

            Match imageMatch = MatchImgTag(htmlText);

            if (imageMatch.Success)
            {
                result = imageMatch.Groups[0].Value.Trim();
                return true;
            }

            return false;
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
        /// Searches the specified HTML text for the first occurrence of a paragraph tag.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>The <see cref="Match"/>.</returns>
        private static Match MatchFirstParagraphTag(string htmlText)
        {
            var match = Regex.Match(htmlText, @"<p>\s*(.+?)\s*</p>");
            return match;
        }

        /// <summary>
        /// Searches the specified HTML text for occurrences of IMG tags.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>The <see cref="Match"/>.</returns>
        private static Match MatchImgTag(string htmlText)
        {
            var match = Regex.Match(htmlText, @"<(img)\b[^>]*>");
            return match;
        }
    }
}
