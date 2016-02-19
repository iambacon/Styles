namespace IAmBacon.Framework.Mvc
{
    using System.Web.Mvc;

    /// <summary>
    /// Represents the properties and methods that are needed in order to render a view that uses ASP.NET Razor syntax.
    /// </summary>
    public abstract class CustomViewPage: WebViewPage
    {
        /// <summary>
        /// Gets or sets the URL of the rendered page.
        /// </summary>
        /// <returns>
        /// The URL of the rendered page.
        /// </returns>
        public new UrlHelperAdapter Url { get; set; }
    }
}
