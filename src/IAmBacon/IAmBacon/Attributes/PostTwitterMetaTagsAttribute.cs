using System;
using System.Web.Mvc;
using IAmBacon.Presentation.Extensions;
using IAmBacon.ViewModels;

namespace IAmBacon.Attributes
{
    public class PostTwitterMetaTagsAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The twitter site username.
        /// </summary>
        private const string TwitterSiteUsername = "@iambacon";

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var view = filterContext.Result as ViewResultBase;
            if (view != null)
            {
                var viewModel = view.ViewData.Model as PostViewModel;
                var metadata = view.ViewData.Model as ITwitterMetadata;

                if (metadata == null || viewModel == null)
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
                metadata.Description = viewModel.Content.GetFirstParagraph();
                metadata.HasImage = viewModel.Image != null;
                if (viewModel.Image != null) metadata.Image = string.Format("{0}{1}", Constants.ContentDeliveryNetwork.Images.ImageUrl, viewModel.Image);
                metadata.MetaTitle = viewModel.PageTitle;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}