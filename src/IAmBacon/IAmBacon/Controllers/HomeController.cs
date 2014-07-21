using System.Collections.Generic;
using System.Web.Mvc;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Model.Entities;
using IAmBacon.ViewModels;
using IAmBacon.ViewModels.Shared;
using WebGrease.Css.Extensions;

namespace IAmBacon.Controllers
{
    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The post service.
        /// </summary>
        private readonly IPostService postService;

        /// <summary>
        /// The recent posts count.
        /// </summary>
        private const int RecentPostsCount = 3;

        /// <summary>
        /// The popular posts count.
        /// </summary>
        private const int PopularPostsCount = 3;

        /// <summary>
        /// The popular posts.
        /// </summary>
        private IEnumerable<Post> popularPosts;

        /// <summary>
        /// The recent posts.
        /// </summary>
        private IEnumerable<Post> recentPosts;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        public HomeController(IPostService postService)
        {
            this.postService = postService;
        }

        #region Public Methods and Operators

        /// <summary>
        /// The home page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            this.recentPosts = this.postService.GetLatest(RecentPostsCount);
            this.popularPosts = this.postService.GetPopular(PopularPostsCount);
            var model = SetHomeViewModel();

            return View("Home", model);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Maps the popular posts.
        /// </summary>
        /// <returns>Returns the list of popular posts.</returns>
        private IEnumerable<PopularPostViewModel> MapPopularPosts()
        {
            var mappedPosts = new List<PopularPostViewModel>();
            this.popularPosts.ForEach(x => mappedPosts.Add(MapPopularPostViewModel(x)));

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

        /// <summary>
        /// Maps the recent posts.
        /// </summary>
        /// <returns>
        /// Returns the list of recent posts.
        /// </returns>
        private IEnumerable<RecentPostViewModel> MapRecentPosts()
        {
            var mappedPosts = new List<RecentPostViewModel>();
            this.recentPosts.ForEach(x => mappedPosts.Add(MapRecentPostViewModel(x)));

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
        /// Sets the footer view model.
        /// </summary>
        /// <returns>The FooterViewModel.</returns>
        private FooterViewModel SetFooterViewModel()
        {
            return new FooterViewModel
            {
                RecentPosts = MapRecentPosts(),
                PopularPosts = MapPopularPosts()
            };
        }

        /// <summary>
        /// Sets the home view model.
        /// </summary>
        /// <returns>
        /// The view model.
        /// </returns>
        private HomeViewModel SetHomeViewModel()
        {
            return new HomeViewModel
            {
                PageTitle = "Colin Bacon, Web Developer - I am Bacon",
                Footer = SetFooterViewModel()
            };
        }

        #endregion
    }
}