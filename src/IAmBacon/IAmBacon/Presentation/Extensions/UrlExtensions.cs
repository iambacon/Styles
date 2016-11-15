namespace IAmBacon.Presentation.Extensions
{
    using IAmBacon.Domain.Utilities;

    /// <summary>
    /// Url extension methods.
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Creates a Gravatar URL.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public static string ToGravatarUrl(this string hash)
        {
            return Constants.ContentDeliveryNetwork.Images.GravitarUrl + hash;
        }

        /// <summary>
        /// Creates an image URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static string ToImageUrl(this string image)
        {
            return Constants.ContentDeliveryNetwork.Images.ImageUrl + image;
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
    }
}