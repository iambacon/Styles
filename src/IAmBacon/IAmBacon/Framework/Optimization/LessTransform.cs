﻿namespace IAmBacon.Framework.Optimization
{
    using System.Web.Optimization;

    /// <summary>
    /// Transformation for LESS files.
    /// </summary>
    public class LessTransform : IBundleTransform
    {
        /// <summary>
        /// Processes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="response">The response.</param>
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = dotless.Core.Less.Parse(response.Content);
            response.ContentType = "text/css";
        }
    }
}