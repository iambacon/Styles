using System.Text.RegularExpressions;

namespace IAmBacon.Core.Domain.Utilities
{
    public static class Seo
    {
        /// <summary>
        /// Returns an seo friendly title used to generate URL for a page from the specified page name.
        /// </summary>
        public static string Title(string name)
        {
            // make the url lowercase.
            string encodedUrl = (name ?? string.Empty).ToLower();

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
    }
}