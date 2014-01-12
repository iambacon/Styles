namespace IAmBacon.Controllers
{
    using System.Web.Mvc;

    using IAmBacon.ViewModels;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        /// The home page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new HomeViewModel { PageTitle = "I am Bacon - Colin Bacon - Web Developer" };
            return View(model);
        }

        #endregion
    }
}