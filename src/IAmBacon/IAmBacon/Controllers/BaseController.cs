using IAmBacon.Framework.Mvc;

namespace IAmBacon.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The base controller.
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Gets the URL helper object that is used to generate URLs by using routing.
        /// </summary>
        public new IUrlHelper Url { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Url = new UrlHelperAdapter(base.Url);
        }
    }
}