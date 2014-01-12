namespace IAmBacon.Areas.Admin
{
    using System.Web.Mvc;

    /// <summary>
    /// The admin area registration.
    /// </summary>
    public class AdminAreaRegistration : AreaRegistration
    {
        #region Public Properties

        /// <summary>
        /// Gets the area name.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The admin area routes.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Home",
                "Admin",
                new { controller = "Home", action = "Index" },
                new[] { "IAmBacon.Areas.Admin.Controllers" });

            context.MapRoute(
                "Admin_Template",
                "Admin/Template/{action}/{id}",
                new { controller = "Template", action = "Index" },
                new { id = @"\d+" },
                new[] { "IAmBacon.Areas.Admin.Controllers" });

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "IAmBacon.Areas.Admin.Controllers" });
        }

        #endregion
    }
}