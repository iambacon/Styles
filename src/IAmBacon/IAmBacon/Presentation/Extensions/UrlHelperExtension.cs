using System.Web.Mvc;
using IAmBacon.Framework.Mvc;

namespace IAmBacon.Presentation.Extensions
{
    /// <summary>
    /// Extension methods to encapsulate <see cref="UrlHelper"/> for generating urls.
    /// </summary>
    public static class UrlHelperExtension
    {
        /// <summary>
        /// Returns the blog post url for the specified title.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="title">The title.</param>
        /// <param name="protocol">The protocol, such as "http" or "https".</param>
        /// <returns>The URL.</returns>
        public static string Post(this IUrlHelper helper, string title, string protocol = "http")
        {
            return helper.RouteUrl("BlogPost", new { title = title.ToSeoUrl() }, protocol);
        }
    }
}