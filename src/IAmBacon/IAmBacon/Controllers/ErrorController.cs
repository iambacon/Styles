using System.Web.Mvc;

namespace IAmBacon.Controllers
{
    using ViewModels;

    /// <summary>
    /// The error controller.
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// The error action method.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// The 404 not found action method.
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            // We set status code to 404 so that robots scanning the URL get the 404.
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;

            var model = new ErrorViewModel { PageTitle = "Bacon not found - I am Bacon" };
            return this.View(model);
        }
    }
}
