﻿namespace IAmBacon
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using IAmBacon.Data.NinjectModules;
    using IAmBacon.Domain.NinjectModules;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;

    /// <summary>
    /// The MVC application.
    /// </summary>
    public class MvcApplication : NinjectHttpApplication
    {
        #region Methods

        /// <summary>
        /// The create kernel.
        /// </summary>
        /// <returns>
        /// The <see cref="IKernel"/>.
        /// </returns>
        protected override IKernel CreateKernel()
        {
            var modules = new INinjectModule[] { new DomainModule(), new DataModule() };
            var kernel = new StandardKernel(modules);

            return kernel;
        }

        /// <summary>
        /// The on application started.
        /// </summary>
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        #endregion
    }
}