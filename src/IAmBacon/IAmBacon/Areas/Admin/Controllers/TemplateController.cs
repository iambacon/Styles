namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The template controller.
    /// </summary>
    public class TemplateController : BaseController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(int id)
        {
            // Todo: This needs to be done properly, like CCTV
            switch (id)
            {
                case 1:
                    return this.View("LandingPage");
                case 2:
                    return this.View("Postv2");
                default:
                    return this.View("Post");
            }
            
        }

        #endregion
    }
}