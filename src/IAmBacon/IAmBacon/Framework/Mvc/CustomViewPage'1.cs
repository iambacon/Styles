namespace IAmBacon.Framework.Mvc
{
    using System.Web.Mvc;

    /// <summary>
    /// Represents the properties and methods that are needed in order to render a view that uses ASP.NET Razor syntax.
    /// </summary>
    /// <typeparam name="TModel">The type of the view data model.</typeparam>
    public abstract class CustomViewPage<TModel> : WebViewPage<TModel>
    {
        /// <summary>
        /// Gets or sets the URL of the rendered page.
        /// </summary>
        /// <returns>
        /// The URL of the rendered page.
        /// </returns>
        public new UrlHelperAdapter Url { get; set; }

        /// <inheritdoc />
        public override void InitHelpers()
        {
            base.InitHelpers();
            this.Url = new UrlHelperAdapter(this.ViewContext.RequestContext);
        }
    }
}
