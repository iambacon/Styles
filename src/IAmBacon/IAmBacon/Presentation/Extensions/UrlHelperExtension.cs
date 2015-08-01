﻿namespace IAmBacon.Presentation.Extensions
{
    using System.Web.Mvc;

    using IAmBacon.Framework.Mvc;

    /// <summary>
    /// Extension methods to encapsulate <see cref="UrlHelper"/> for generating URLs.
    /// </summary>
    public static class UrlHelperExtension
    {
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