namespace IAmBacon.Domain.Utilities
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Seo utility class.
    /// </summary>
    public static class Seo
    {
        #region Public Methods and Operators

        /// <summary>
        /// Create an SEO friendly URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string SeoUrl(string url)
        {
            // make the url lowercase.
            string encodedUrl = (url ?? string.Empty).ToLower();

            // replace C# with C Sharp.
            encodedUrl = Regex.Replace(encodedUrl, "c#", "c sharp", RegexOptions.IgnoreCase);

            // replace & with and.
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters.
            encodedUrl = encodedUrl.Replace("'", string.Empty);

            // remove invalid characters.
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates.
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters.
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        #endregion
    }
}