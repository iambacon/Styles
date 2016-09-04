﻿namespace IAmBacon.Attributes
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Requires that a HTTP request does not contain a trailing slash. If it does, return a 404 Not Found. This is
    /// useful if you are dynamically generating something which acts like it's a file on the web server.
    /// E.g. /Robots.txt/ should not have a trailing slash and should be /Robots.txt. Note, that we also don't care if
    /// it is upper-case or lower-case in this instance.
    /// </summary>
    /// <remarks>
    /// Full attribution for this attribute goes to Rehan Saeed.
    /// https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate
    /// </remarks>
    public class NoTrailingSlashAttribute : FilterAttribute, IAuthorizationFilter
    {
        private const char QueryCharacter = '?';
        private const char SlashCharacter = '/';

        /// <summary>
        /// Determines whether a request contains a trailing slash and if it does, calls the
        /// <see cref="HandleTrailingSlashRequest"/> method.
        /// </summary>
        /// <param name="filterContext">An object that encapsulates information that is required in order to use the
        /// <see cref="RequireHttpsAttribute"/> attribute.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            string canonicalUrl = filterContext.HttpContext.Request.Url.ToString();
            int queryIndex = canonicalUrl.IndexOf(QueryCharacter);

            if (queryIndex == -1)
            {
                if (canonicalUrl[canonicalUrl.Length - 1] == SlashCharacter)
                {
                    this.HandleTrailingSlashRequest(filterContext);
                }
            }
            else
            {
                if (canonicalUrl[queryIndex - 1] == SlashCharacter)
                {
                    this.HandleTrailingSlashRequest(filterContext);
                }
            }
        }

        /// <summary>
        /// Handles HTTP requests that have a trailing slash but are not meant to.
        /// </summary>
        /// <param name="filterContext">An object that encapsulates information that is required in order to use the
        /// <see cref="RequireHttpsAttribute"/> attribute.</param>
        private void HandleTrailingSlashRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}