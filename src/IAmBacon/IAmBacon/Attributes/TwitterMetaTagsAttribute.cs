using System;
using System.Web.Mvc;
using IAmBacon.ViewModels;

namespace IAmBacon.Attributes
{
    /// <summary>
    /// The Twitter meta tags attribute.
    /// </summary>
    public class TwitterMetaTagsAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The base URL for the image CDN.
        /// </summary>
        private const string BaseImageUrl = "http://images.iambacon.co.uk";

        /// <summary>
        /// The twitter site username.
        /// </summary>
        private const string TwitterSiteUsername = "@iambacon";

        /// <summary>
        /// The twitter image name.
        /// </summary>
        private const string TwitterImageName = "twitter-card.png";

        /// <summary>
        /// The page description field.
        /// </summary>
        private readonly string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterMetaTagsAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public TwitterMetaTagsAttribute(string description)
        {
            _description = description;
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var view = filterContext.Result as ViewResultBase;
            if (view != null)
            {
                var viewModel = view.ViewData.Model as ViewModelBase;
                var metadata = view.ViewData.Model as ITwitterMetadata;

                if (metadata == null)
                {
                    base.OnActionExecuted(filterContext);
                    return;
                }

                Uri uri = filterContext.RequestContext.HttpContext.Request.Url;
                string canonicalUrl = string.Empty;

                if (uri != null)
                {
                    canonicalUrl = uri.GetLeftPart(UriPartial.Path);
                }

                metadata.Site = TwitterSiteUsername;
                metadata.Url = canonicalUrl;
                metadata.Description = _description;
                metadata.Image = string.Format("{0}/{1}", BaseImageUrl, TwitterImageName);
                if (viewModel != null) metadata.Title = viewModel.PageTitle;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}