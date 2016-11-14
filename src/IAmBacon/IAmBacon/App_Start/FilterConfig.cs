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
            AddSecurityFilters(filters);
        }

        private static void AddSearchEngineOptimizationFilters(GlobalFilterCollection filters)
        {
            filters.Add(
                new RedirectToCanonicalUrlAttribute(
                    RouteTable.Routes.AppendTrailingSlash,
                    RouteTable.Routes.LowercaseUrls));
        }

        private static void AddSecurityFilters(GlobalFilterCollection filters)
        {
            // Require HTTPS to be used across the whole site. System.Web.Mvc.RequireHttpsAttribute performs a
            // 302 Temporary redirect from a HTTP URL to a HTTPS URL. This filter gives you the option to perform a
            // 301 Permanent redirect or a 302 temporary redirect. You should perform a 301 permanent redirect if the
            // page can only ever be accessed by HTTPS and a 302 temporary redirect if the page can be accessed over
            // HTTP or HTTPS.
            filters.Add(new RedirectToHttpsAttribute(true));
        }
    }
}