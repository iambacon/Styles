using System.Web.Mvc;
using System.Web.Routing;

namespace IAmBacon.Framework.Mvc
{
    /// <summary>
    /// Wrapper for UrlHelper.
    /// </summary>
    public class UrlHelperAdapter : UrlHelper, IUrlHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlHelperAdapter"/> class.
        /// </summary>
        /// <param name="requestContext">An object that contains information about the current request and about the route that it matched.</param>
        public UrlHelperAdapter(RequestContext requestContext)
            : base(requestContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlHelperAdapter"/> class.
        /// </summary>
        /// <param name="requestContext">An object that contains information about the current request and about the route that it matched.</param>
        /// <param name="routeCollection">A collection of routes.</param>
        public UrlHelperAdapter(RequestContext requestContext, RouteCollection routeCollection)
            : base(requestContext, routeCollection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlHelperAdapter"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public UrlHelperAdapter(UrlHelper helper)
            : base(helper.RequestContext, helper.RouteCollection)
        {
        }
    }
}