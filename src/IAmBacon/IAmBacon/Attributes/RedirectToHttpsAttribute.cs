﻿namespace IAmBacon.Attributes
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Represents an attribute that forces an unsecured HTTP request to be re-sent over HTTPS.
    /// <see cref="RequireHttpsAttribute"/> performs a 302 Temporary redirect from a HTTP URL to a HTTPS URL. This
    /// filter gives you the option to perform a 301 Permanent redirect or a 302 temporary redirect. You should
    /// perform a 301 permanent redirect if the page can only ever be accessed by HTTPS and a 302 temporary redirect if
    /// the page can be accessed over HTTP or HTTPS. <see cref="RequireHttpsAttribute"/> also throws an
    /// <see cref="InvalidOperationException"/> if request is made except GET, which returns a 500 Internal Server
    /// Error to the client. This filter, returns a 405 Method Not Allowed instead, which is much more suitable.
    /// </summary>
    /// <remarks>
    /// Full attribution for this attribute goes to Rehan Saeed.
    /// https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate
    /// </remarks>
    public class RedirectToHttpsAttribute : FilterAttribute, IAuthorizationFilter
    {
        public RedirectToHttpsAttribute(bool permanent)
        {
            Permanent = permanent;
        }

        public bool Permanent { get; }

        /// <summary>
        /// Determines whether a request is secured (HTTPS) and, if it is not, calls the
        /// <see cref="HandleNonHttpsRequest"/> method.
        /// </summary>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException(nameof(filterContext));

            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                this.HandleNonHttpsRequest(filterContext);
            }
        }

        protected virtual void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            // Only redirect for GET requests, otherwise the browser might not propagate the verb and request body correctly.
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, WebRequestMethods.Http.Get, StringComparison.OrdinalIgnoreCase))
            {
                // The RequireHttpsAttribute throws an InvalidOperationException. Some bots and spiders make HEAD
                // requests (to reduce bandwidth) and we don't want them to see a 500-Internal Server Error. A 405
                // Method Not Allowed would be more appropriate.
                throw new HttpException((int)HttpStatusCode.Forbidden, "Forbidden");
            }

            string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(url, Permanent);
        }
    }
}