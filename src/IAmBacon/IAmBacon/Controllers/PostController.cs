﻿
namespace IAmBacon.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Web;
    using System.Web.Mvc;

    using IAmBacon.Domain.Services.Interfaces;
    using IAmBacon.Framework.Mvc;
    using IAmBacon.Model.Entities;
    using IAmBacon.Models;
    using IAmBacon.Presentation.Extensions;
    using IAmBacon.Presentation.Helpers;
    using IAmBacon.ViewModels;

    /// <summary>
    /// The post controller.
    /// </summary>
    public class PostController : BaseController
    {
        #region Fields

        /// <summary>
        /// The post service.
        /// </summary>
        private readonly IPostService postService;

        /// <summary>
        /// The category service.
        /// </summary>
        private readonly ICategoryService categoryService;

        /// <summary>
        /// The comment service.
        /// </summary>
        private readonly ICommentService commentService;

        /// <summary>
        /// The tag service.
        /// </summary>
        private readonly ITagService tagService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController" /> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        /// <param name="commentService">The comment service.</param>
        /// <param name="tagService">The tag service.</param>
        /// <param name="categoryService">The category service.</param>
        public PostController(IPostService postService, ICommentService commentService, ITagService tagService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.tagService = tagService;
            this.categoryService = categoryService;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            // TODO: Use automapper.
            var posts = this.postService.GetLatest();

            var postModels = CreatePostModels(posts);

            var model = new PostsViewModel { Posts = postModels, PageTitle = "I am Blog - I am Bacon" };

            return this.View(model);
        }

        /// <summary>
        /// Retrieves the category by the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ActionResult Category(string name)
        {
            var category = this.categoryService.Get(x => x.SeoName == name).FirstOrDefault();

            if (category == null)
            {
                return this.RedirectToAction("NotFound", "Error");
            }

            var posts = this.postService.Get(x => x.CategoryId == category.Id).Where(x => x.Active);
            var postModels = CreatePostModels(posts);

            var model = new PostsViewModel
                {
                    PageTitle = string.Format("Category: {0}  - I am Bacon", name),
                    Posts = postModels,
                    Title = category.Name
                };

            return this.View(model);

        }

        /// <summary>
        /// Retrieves the post by the specified id.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Details(string title, int id = 0)
        {
            var post = this.postService.Get(title);

            if (post == null && id > 0)
            {
                // This is here for legacy routes.
                post = this.postService.Get(id);
            }

            if (post == null || !post.Active)
            {
                return this.RedirectToAction("NotFound", "Error");
            }

            var comments =
                post.Comments.Select(x => new CommentModel
                {
                    Content = x.Content,
                    GravitarUrl = x.Hash.ToGravatarUrl(),
                    Name = x.Name,
                    DateTime = x.DateCreated.ToDateTimeFormat(),
                    DateCreated = x.DateCreated.ToDisplayDateTime()
                }).OrderByDescending(x => x.DateCreated);

            // TODO: Use automapper
            // TODO: Don't use entities in presentation layer
            // TODO: Need a better way of doing page titles
            var model = new PostViewModel
                {
                    Title = post.Title,
                    SeoTitle = post.SeoTitle,
                    Content = new HtmlString(post.Content),
                    DateTime = post.DateCreated.ToDateTimeFormat(),
                    DateCreated = post.DateCreated.ToDisplayDateTime(),
                    Tags = post.Tags,
                    Author = post.User.FirstName + " " + post.User.LastName,
                    Category = post.Category.SeoName,
                    Comments = comments.ToList(),
                    NoCss = post.NoCss,
                    Id = post.Id,
                    PageTitle = post.Title + " - I am Bacon"
                };

            return this.View(model);
        }

        /// <summary>
        /// The RSS feed action method.
        /// </summary>
        /// <returns></returns>
        public ActionResult Feed()
        {
            const string BlogTitle = "I am Blog";
            const string BlogDescription = "The blog feed for Colin Bacon - web developer.";
            const string BlogUrl = "http://www.iambacon.co.uk/blog";

            // Create a collection of SyndicationItemobjects from the latest posts
            var posts = this.postService.GetLatest().Select
            (
              p => new SyndicationItem
                  (
                      p.Title,
                      p.Content,
                  // TODO: Url generation needs to be a LOT better.
                      new Uri(string.Concat(BlogUrl, "/", p.SeoTitle, "-", p.Id))
                  )
            );

            // Create an instance of SyndicationFeed class passing the SyndicationItem collection
            var feed = new SyndicationFeed(BlogTitle, BlogDescription, new Uri(BlogUrl), posts)
            {
                Copyright = new TextSyndicationContent(String.Format("Copyright © {0}", BlogTitle)),
                Language = "en-GB"
            };

            // Format feed in RSS format through Rss20FeedFormatter formatter
            var feedFormatter = new Rss20FeedFormatter(feed);

            // Call the custom action that write the feed to the response
            return new FeedResult(feedFormatter);
        }

        /// <summary>
        /// Saves the comment.
        /// </summary>
        /// <param name="newComment">The new comment.</param>
        /// <param name="id">The post id.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public ActionResult SaveComment(CommentModel newComment, int id, string title)
        {
            if (ModelState.IsValid)
            {
                // TODO: Use Automapper
                var hash = newComment.Email != null ?
                    EncryptionHelper.GetMd5Hash(newComment.Email.ToLower().Trim()) :
                    null;

                var entity = new Comment
                {
                    Content = newComment.Content,
                    Hash = hash,
                    Name = newComment.Name,
                    Url = newComment.Url,
                    PostId = id
                };

                this.commentService.Create(entity);
            }

            // TODO: Url generation needs improving, so we don't forget to sanitise the title.
            return this.RedirectToRoute("BlogPost", new { title = title.ToSeoUrl() });
        }

        /// <summary>
        /// Gets the posts by the specified tag name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ActionResult Tag(string name)
        {
            var tag = this.tagService.Get(x => x.SeoName == name).FirstOrDefault();

            if (tag != null)
            {
                var postModels = CreatePostModels(tag.Posts.Where(x => x.Active));

                var model = new PostsViewModel
                    {
                        Posts = postModels,
                        PageTitle = string.Format("Tag: {0}  - I am Bacon", name),
                        Title = tag.Name
                    };

                return this.View(model);
            }

            return this.RedirectToAction("Index");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a list of post view models.
        /// </summary>
        /// <param name="posts">The posts.</param>
        /// <returns>IEnumerable of PostViewModel.</returns>
        private static IEnumerable<PostViewModel> CreatePostModels(IEnumerable<Post> posts)
        {
            var postModels =
                posts.Select(
                    x =>
                    new PostViewModel
                        {
                            Title = x.Title,
                            SeoTitle = x.SeoTitle,
                            Content = x.Content.GetFirstParagraph(),
                            DateCreated = x.DateCreated.ToDisplayDateTime(),
                            DateTime = x.DateCreated.ToDateTimeFormat(),
                            Tags = x.Tags,
                            Author = x.User.FirstName + " " + x.User.LastName,
                            Category = x.Category.Name,
                            Id = x.Id
                        });
            return postModels;
        }

        #endregion

    }
}