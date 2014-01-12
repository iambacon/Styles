namespace IAmBacon.Attributes
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// The canonical url attribute.
    /// </summary>
    public class CanonicalUrlAttribute : ActionFilterAttribute
    {
        #region Fields

        /// <summary>
        /// The URL.
        /// </summary>
        private readonly string url;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CanonicalUrlAttribute"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public CanonicalUrlAttribute(string url)
        {
            // TODO: This is not finished!!    
            this.url = url;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var uri = filterContext.RequestContext.HttpContext.Request.Url;
            if (uri != null)
            {
                var fullyQualifiedUrl = (uri.Scheme + "://" + uri.Host + this.url).ToLowerInvariant();

                fullyQualifiedUrl = filterContext.RouteData.Values.Keys.Aggregate(
                    fullyQualifiedUrl,
                    (current, key) =>
                    current.Replace("{" + key.ToLowerInvariant() + "}", filterContext.RouteData.Values[key].ToString()));

                filterContext.Controller.ViewData["CanonicalUrl"] = fullyQualifiedUrl;
                filterContext.HttpContext.Response.Headers.Add("link", "<" + fullyQualifiedUrl + ">; rel=\"canonical\"");
            }

            base.OnActionExecuting(filterContext);
        }

        #endregion
    }
}