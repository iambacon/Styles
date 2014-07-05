using System.Web.Mvc;

namespace IAmBacon.Controllers
{
    public class StyleguideController : Controller
    {
        //
        // GET: /Styleguide/

        public ActionResult Index()
        {
            return View("Styleguide");
        }

    }
}
