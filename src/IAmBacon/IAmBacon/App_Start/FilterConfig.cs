namespace IAmBacon
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using IAmBacon.Attributes;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            AddSearchEngineOptimizationFilters(filters);
        }

        private static void AddSearchEngineOptimizationFilters(GlobalFilterCollection filters)
        {
            filters.Add(
                new RedirectToCanonicalUrlAttribute(
                    RouteTable.Routes.AppendTrailingSlash,
                    RouteTable.Routes.LowercaseUrls));
        }
    }
}