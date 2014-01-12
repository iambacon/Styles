using System.Web;
using System.Web.Mvc;
using System.Linq;
using StackExchange.Profiling;
using StackExchange.Profiling.MVCHelpers;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(IAmBacon.App_Start.MiniProfilerPackage), "PreStart")]

[assembly: WebActivator.PostApplicationStartMethod(
    typeof(IAmBacon.App_Start.MiniProfilerPackage), "PostStart")]


namespace IAmBacon.App_Start
{
    public static class MiniProfilerPackage
    {
        public static void PreStart()
        {
            //TODO: Non SQL Server based installs can use other formatters like: new StackExchange.Profiling.SqlFormatters.InlineFormatter()
            MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.SqlServerFormatter();

            //TODO: To profile a standard DbConnection: 
            // var profiled = new ProfiledDbConnection(cnn, MiniProfiler.Current);

            MiniProfilerEF.Initialize();

            //Make sure the MiniProfiler handles BeginRequest and EndRequest
            DynamicModuleUtility.RegisterModule(typeof(MiniProfilerStartupModule));

            //Setup profiler for Controllers via a Global ActionFilter
            GlobalFilters.Filters.Add(new ProfilingActionFilter());

            // the list of all sessions in the store is restricted by default, you must return true to alllow it
            //MiniProfiler.Settings.Results_List_Authorize = (request) =>
            //{
            // you may implement this if you need to restrict visibility of profiling lists on a per request basis 
            //return true; // all requests are kosher
            //};
        }

        public static void PostStart()
        {
            // Intercept ViewEngines to profile all partial views and regular views.
            // If you prefer to insert your profiling blocks manually you can comment this out
            ////var copy = ViewEngines.Engines.ToList();
            ////ViewEngines.Engines.Clear();
            ////foreach (var item in copy)
            ////{
            ////    ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            ////}
        }
    }

    public class MiniProfilerStartupModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) =>
            {
                var request = ((HttpApplication)sender).Request;
                //TODO: By default only local requests are profiled, optionally you can set it up
                //  so authenticated users are always profiled
                if (request.IsLocal)
                {
                    MiniProfiler.Start();
                }
            };

            context.EndRequest += (sender, e) => MiniProfiler.Stop();
        }

        public void Dispose() { }
    }
}

