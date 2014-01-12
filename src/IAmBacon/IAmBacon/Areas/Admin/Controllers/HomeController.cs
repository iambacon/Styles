namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : BaseController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }
        #endregion
    }
}