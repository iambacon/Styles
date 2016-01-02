namespace IAmBacon.Presentation.Extensions
{
    using System.Web.Mvc;

    using IAmBacon.Framework.Mvc;

    /// <summary>
    /// Extension methods to encapsulate <see cref="UrlHelper"/> for generating URLs.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Returns the blog landing page URL by the specified page no.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="protocol">The protocol.</param>
        /// <returns>The blog landing page URL by the specified page no.</returns>
        public static string Blog(this IUrlHelper helper, int pageNo, string protocol = "http")
        {
            return helper.RouteUrl("BlogHome", new { page = pageNo }, protocol);
        }

        /// <summary>
        /// Returns the blog post URL for the specified title.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="title">The title.</param>
        /// <param name="protocol">The protocol, such as "http" or "https".</param>
        /// <returns>The URL.</returns>
        public static string Post(this IUrlHelper helper, string title, string protocol = "http")
        {
            return helper.RouteUrl("BlogPost", new { title = title.ToSeoUrl() }, protocol);
        }

        /// <summary>
        /// Returns the tag URL for the specified tag name.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of the tag.</param>
        /// <param name="protocol">The protocol.</param>
        /// <returns>The URL.</returns>
        public static string Tag(this IUrlHelper helper, string name, string protocol = "http")
        {
            return helper.RouteUrl("Tag", new { name }, protocol);
        }
    }
}