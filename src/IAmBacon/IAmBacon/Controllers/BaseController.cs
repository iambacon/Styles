using System.Collections.Generic;
using System.Linq;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Framework.Mvc;
using IAmBacon.Model.Entities;
using IAmBacon.ViewModels.Shared;

namespace IAmBacon.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The base controller.
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// The popular posts count.
        /// </summary>
        private const int PopularPostsCount = 3;

        /// <summary>
        /// The recent posts count.
        /// </summary>
        private const int RecentPostsCount = 3;

        /// <summary>
        /// The post service.
        /// </summary>
        private readonly IPostService _postService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        protected BaseController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Gets the URL helper object that is used to generate URLs by using routing.
        /// </summary>
        public new IUrlHelper Url { get; set; }

        /// <summary>
        /// Initializes data that might not be available when the constructor is called.
        /// </summary>
        /// <param name="requestContext">The HTTP context and route data.</param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Url = new UrlHelperAdapter(base.Url);
        }

        /// <summary>
        /// The page footer.
        /// </summary>
        /// <returns>The partial view.</returns>
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var recentPosts = _postService.GetLatest(RecentPostsCount);
            var popularPosts = _postService.GetPopular(PopularPostsCount);
            var model = SetFooterViewModel(recentPosts, popularPosts);

            return PartialView("_Footer", model);
        }

        /// <summary>
        /// Sets the footer view model.
        /// </summary>
        /// <returns>The FooterViewModel.</returns>
        private FooterViewModel SetFooterViewModel(IEnumerable<Post> recentPosts, IEnumerable<Post> popularPosts)
        {
            return new FooterViewModel
            {
                RecentPosts = MapRecentPosts(recentPosts),
                PopularPosts = MapPopularPosts(popularPosts)
            };
        }

        /// <summary>
        /// Maps the recent posts.
        /// </summary>
        /// <returns>
        /// Returns the list of recent posts.
        /// </returns>
        private IEnumerable<RecentPostViewModel> MapRecentPosts(IEnumerable<Post> recentPosts)
        {
            var mappedPosts = new List<RecentPostViewModel>();
            recentPosts.ToList().ForEach(x => mappedPosts.Add(MapRecentPostViewModel(x)));

            return mappedPosts;
        }

        /// <summary>
        /// Maps the recent post view model.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>The RecentPostViewModel.</returns>
        private static RecentPostViewModel MapRecentPostViewModel(Post post)
        {

            return new RecentPostViewModel
            {
                Title = post.Title,
                SeoTitle = post.SeoTitle
            };
        }

        /// <summary>
        /// Maps the popular posts.
        /// </summary>
        /// <returns>Returns the list of popular posts.</returns>
        private IEnumerable<PopularPostViewModel> MapPopularPosts(IEnumerable<Post> popularPosts)
        {
            var mappedPosts = new List<PopularPostViewModel>();
            popularPosts.ToList().ForEach(x => mappedPosts.Add(MapPopularPostViewModel(x)));

            return mappedPosts;
        }

        /// <summary>
        /// Maps the popular post view model.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>The PopularPostViewModel.</returns>
        private static PopularPostViewModel MapPopularPostViewModel(Post post)
        {
            return new PopularPostViewModel
            {
                Title = post.Title,
                SeoTitle = post.SeoTitle
            };
        }
    }
}