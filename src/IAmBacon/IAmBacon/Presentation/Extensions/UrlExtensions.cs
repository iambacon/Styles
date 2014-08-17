namespace IAmBacon.Presentation.Extensions
{
    using IAmBacon.Domain.Utilities;

    /// <summary>
    /// Url extension methods.
    /// </summary>
    public static class UrlExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates a Gravatar URL.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public static string ToGravatarUrl(this string hash)
        {
            const string GravatarUrl = "http://www.gravatar.com/avatar/";

            return GravatarUrl + hash;
        }

        /// <summary>
        /// Creates an image URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static string ToImageUrl(this string image)
        {
            const string ImageUrl = "http://images.iambacon.co.uk/blog/";

            return ImageUrl + image;
        }

        /// <summary>
        /// Sanitises string to form SEO friendly url string.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string ToSeoUrl(this string url)
        {
            return Seo.SeoUrl(url);
        }

        #endregion
    }
}