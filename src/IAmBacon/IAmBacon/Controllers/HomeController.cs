using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Model.Entities;
using IAmBacon.Presentation.Extensions;
using IAmBacon.ViewModels.Home;

namespace IAmBacon.Controllers
{
    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// The post service.
        /// </summary>
        private readonly IPostService postService;

        /// <summary>
        /// The latest posts count.
        /// </summary>
        private const int LatestPostsCount = 8;

        /// <summary>
        /// The latest posts.
        /// </summary>
        private IEnumerable<Post> latestPosts;

        /// <summary>
        /// The popular posts.
        /// </summary>
        private IEnumerable<Post> popularPosts;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        public HomeController(IPostService postService)
            : base(postService)
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
            this.latestPosts = this.postService.GetLatest(LatestPostsCount);

            var model = SetHomeViewModel();

            return View("Home", model);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Maps the post thumb view model.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        private static PostThumbViewModel MapPostThumbViewModel(Post post)
        {
            return new PostThumbViewModel
            {
                Title = post.Title,
                Thumbnail = post.Image.ToImageUrl(),
                DateTime = post.DateCreated.ToDateTimeFormat(),
                DisplayDate = post.DateCreated.ToDisplayDate(),
                Category = post.Category.Name,
                SeoTitle = post.SeoTitle,
                Author = post.User.FirstName + " " + post.User.LastName,
            };
        }

        /// <summary>
        /// Sets the blog posts.
        /// </summary>
        /// <returns>The list of post thumbs.</returns>
        private IEnumerable<PostThumbViewModel> SetBlogPosts()
        {
            return this.latestPosts != null
                ? this.latestPosts.Select(MapPostThumbViewModel).ToList()
                : null;
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
                BlogPosts = SetBlogPosts()
            };
        }

        #endregion
    }
}