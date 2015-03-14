namespace IAmBacon
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        #region Public Methods and Operators

        /// <summary>
        /// The application routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Feed",
                "feed",
                new { controller = "Post", action = "feed" },
                new[] { "IAmBacon.Controllers" });

            routes.MapRoute(
                "Rss",
                "rss",
                new { controller = "Post", action = "feed" },
                new[] { "IAmBacon.Controllers" });

            routes.MapRoute(
                "BlogPostLegacy",
                "blog/{title}-{id}",
                new { controller = "Post", action = "Details" },
                new { id = @"\d+" },
                new[] { "IAmBacon.Controllers" });

            routes.MapRoute(
                "BlogPost",
                "blog/{title}",
                new { controller = "Post", action = "Details" },
                new[] { "IAmBacon.Controllers" });

            routes.MapRoute(
                "Category",
                "blog/category/{name}",
                new { controller = "Post", action = "Category" },
                new[] { "IAmBacon.Controllers" });

            routes.MapRoute(
                "Tag",
                "blog/tag/{name}",
                new { controller = "Post", action = "Tag" },
                new[] { "IAmBacon.Controllers" });

            routes.MapRoute(
                "BlogHome",
                "blog",
                new { controller = "Post", action = "Index" },
                new[] { "IAmBacon.Controllers" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "IAmBacon.Controllers" });
        }

        #endregion
    }
}