namespace IAmBacon.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Web;
    using System.Web.Mvc;

    using Domain.Services.Interfaces;
    using Framework.Mvc;

    using Presentation.Mappers;
    using Attributes;
    using Domain.Smtp.Interfaces;
    using Presentation.Builders;
    using ViewModels.Post;
    using ViewModels.Shared;

    using Model.Entities;
    using Models;

    using PagedList;

    using Presentation.Extensions;
    using Presentation.Helpers;
    using ViewModels;

    /// <summary>
    /// The post controller.
    /// </summary>
    public class PostController : BaseController
    {
        /// <summary>
        /// The post service.
        /// </summary>
        private readonly IPostService postService;

        /// <summary>
        /// The category service.
        /// </summary>
        private readonly IService<Category> categoryService;

        /// <summary>
        /// The comment service.
        /// </summary>
        private readonly IService<Comment> commentService;

        /// <summary>
        /// The email manager.
        /// </summary>
        private readonly IEmailManager emailManager;

        /// <summary>
        /// The maximum page numbers to display for pagination.
        /// </summary>
        private const int MaxPageNumbersToDisplay = 5;
        
        /// <summary>
        /// The tag service.
        /// </summary>
        private readonly IService<Tag> tagService;

        /// <summary>
        /// The number of posts per page.
        /// </summary>
        private const int PageSize = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController" /> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        /// <param name="commentService">The comment service.</param>
        /// <param name="tagService">The tag service.</param>
        /// <param name="categoryService">The category service.</param>
        /// <param name="emailManager">The email manager.</param>
        public PostController(
            IPostService postService,
            IService<Comment> commentService,
            IService<Tag> tagService,
            IService<Category> categoryService,
            IEmailManager emailManager)
            : base(postService)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.tagService = tagService;
            this.categoryService = categoryService;
            this.emailManager = emailManager;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(int page = 1)
        {
            IEnumerable<Post> postEntities = this.postService.GetAllActive().OrderByDescending(x => x.DateCreated);
            IPagedList<PostThumbViewModel> pagedPosts = postEntities.ToPagedViewModelListWithContent(this.Url, PageSize, page);
            IEnumerable<Category> categories = this.categoryService.GetAll();
            IEnumerable<Tag> tags = this.tagService.GetAll();
            List<CategoryCountViewModel> categorySummaries = this.GetCategorySummaries(postEntities, categories);
            IEnumerable<TagViewModel> tagViewModels = tags.ToTagViewModelList(this.Url).OrderBy(x => x.Name);

            var model = new PostsViewModel
            {
                Posts = pagedPosts,
                PageTitle = "I am Blog - I am Bacon",
                Footer = new FooterViewModel(),
                CategorySummaries = categorySummaries,
                Tags = tagViewModels,
                Pagination = this.BuildPagination(pagedPosts, this.Url.Blog())
            };

            return this.View("Landing", model);
        }

        /// <summary>
        /// Retrieves the category by the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="page">The page.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Category(string name, int page = 1)
        {
            Category category = this.categoryService.Get(x => x.SeoName == name).FirstOrDefault();

            if (category == null)
            {
                return this.RedirectToAction("NotFound", "Error");
            }

            IEnumerable<Post> posts =
                this.postService.Get(x => x.CategoryId == category.Id)
                    .Where(x => x.Active)
                    .OrderByDescending(x => x.DateCreated);

            IPagedList<PostThumbViewModel> pagedPosts = posts.ToPagedViewModelList(this.Url, PageSize, page);

            foreach (var postThumbViewModel in pagedPosts)
            {
                postThumbViewModel.DisplayCategory = false;
                postThumbViewModel.DisplayTags = true;
            }

            var model = new PostsViewModel
            {
                PageTitle = string.Format("Category: {0}  - I am Bacon", name),
                Posts = pagedPosts,
                Title = category.Name,
                Footer = new FooterViewModel(),
                Pagination = this.BuildPagination(pagedPosts, this.Url.Category(name))
            };

            return this.View(model);

        }

        /// <summary>
        /// Retrieves the post by the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        [PostTwitterMetaTags]
        public ActionResult Details(string title)
        {
            var post = this.postService.Get(title);

            if (post == null || !post.Active)
            {
                return this.RedirectToAction("NotFound", "Error");
            }

            var comments =
                post.Comments.Where(x => x.Active).Select(x => new CommentModel
                {
                    Content = x.Content,
                    GravitarUrl = x.Hash.ToGravatarUrl(),
                    Name = x.Name,
                    DateTime = x.DateCreated.ToDateTimeFormat(),
                    DateCreated = x.DateCreated.ToDisplayDateTime()
                }).OrderByDescending(x => x.DateCreated);

            var tags = this.MapToTagViewModel(post.Tags);

            var model = new PostViewModel
            {
                Title = post.Title,
                SeoTitle = post.SeoTitle,
                Content = new HtmlString(post.Content),
                DateTime = post.DateCreated.ToDateTimeFormat(),
                DateCreated = post.DateCreated.ToDisplayDateTime(),
                Tags = tags,
                Author = post.User.FirstName + " " + post.User.LastName,
                Category = post.Category.SeoName,
                Comments = comments.ToList(),
                NoCss = post.NoCss,
                Id = post.Id,
                PageTitle = post.Title + " - I am Bacon",
                Footer = new FooterViewModel(),
                Image = post.Image
            };


            // TODO: Temporary until we sort out spam
            model.CommentsActive = false;

            return this.View(model);
        }

        /// <summary>
        /// The RSS feed action method.
        /// </summary>
        /// <returns></returns>
        public ActionResult Feed()
        {
            const string blogTitle = "I am Blog";
            const string blogDescription = "The blog feed for Colin Bacon - web developer.";
            
            // Create a collection of SyndicationItemobjects from the latest posts
            var posts = postService.GetLatest(PageSize).Select
                (
                    p => new SyndicationItem
                        (
                        p.Title,
                        p.Content,
                        new Uri(this.Url.Post(p.SeoTitle))
                        )
                );

            // Create an instance of SyndicationFeed class passing the SyndicationItem collection
            var feed = new SyndicationFeed(blogTitle, blogDescription, new Uri(this.Url.Blog()), posts)
            {
                Copyright = new TextSyndicationContent(String.Format("Copyright © {0}", blogTitle)),
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
        [ValidateAntiForgeryToken]
        public ActionResult SaveComment(CommentModel newComment, int id, string title)
        {
            // TODO: temporary until we sort out spam
            if (false)
            {
                if (ModelState.IsValid)
                {
                    // TODO: Use Automapper
                    var hash = newComment.Email != null
                        ? EncryptionHelper.GetMd5Hash(newComment.Email.ToLower().Trim())
                        : null;

                    var entity = new Comment
                    {
                        Content = newComment.Content,
                        Hash = hash,
                        Name = newComment.Name,
                        Url = newComment.Url,
                        PostId = id
                    };

                    // TODO: Should this be in the comment service? Controller shouldn't have to know about it.
                    ////var isActive = this.spamManager.VerifyComment(entity);

                    ////entity.Active = isActive;

                    this.commentService.Create(entity);

                    // Send email notification to me.
                    ////var commentUrl = Url.Action("Index", "Comment", new { area = "Admin" }, Request.Url.Scheme);
                    ////var post = this.postService.Get(id);
                    ////var emailTemplate = EmailTemplateBuilder.NewCommentEmail(entity, post.Title, commentUrl,
                    ////    "View comment");
                    ////this.emailManager.SendNewCommentEmail(emailTemplate.Subject, emailTemplate.Body,
                    ////    emailTemplate.IsHtml);
                }
            }

            return this.RedirectToRoute(Url.Post(title));
        }

        /// <summary>
        /// Gets the posts by the specified tag name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="page">The page.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Tag(string name, int page = 1)
        {
            Tag tag = this.tagService.Get(x => x.SeoName == name).FirstOrDefault();

            if (tag == null)
            {
                return this.RedirectToAction("Index");
            }

            IPagedList<PostThumbViewModel> pagedPosts =
                tag.Posts.Where(x => x.Active)
                    .OrderByDescending(x => x.DateCreated)
                    .ToPagedViewModelList(this.Url, PageSize, page);

            var model = new PostsViewModel
            {
                Posts = pagedPosts,
                PageTitle = string.Format("Tag: {0}  - I am Bacon", name),
                Title = tag.Name,
                Footer = new FooterViewModel(),
                Pagination = this.BuildPagination(pagedPosts, this.Url.Tag(name))
            };

            return this.View(model);
        }

        /// <summary>
        /// Gets the category summaries.
        /// </summary>
        /// <param name="postEntities">The post entities.</param>
        /// <param name="categories">The categories.</param>
        /// <returns>The list of <see cref="CategoryCountViewModel"/>.</returns>
        private List<CategoryCountViewModel> GetCategorySummaries(
            IEnumerable<Post> postEntities,
            IEnumerable<Category> categories)
        {
            List<CategoryCountViewModel> categorySummaries =
                postEntities.GroupBy(x => x.Category.Name)
                    .Select(
                        x =>
                        new CategoryCountViewModel
                        {
                            Name = x.Key,
                            Count = x.Count(),
                            Percent = (double)x.Count() / categories.Count()
                        })
                    .OrderByDescending(x => x.Count)
                    .ToList();

            return categorySummaries;
        }

        /// <summary>
        /// Maps <see cref="Tag"/> to <see cref="TagViewModel"/>.
        /// </summary>
        /// <param name="tags">The <see cref="Tag"/>.</param>
        /// <returns>The <see cref="TagViewModel"/>.</returns>
        private IEnumerable<TagViewModel> MapToTagViewModel(IEnumerable<Tag> tags)
        {
            return tags.Select(x => new TagViewModel
            {
                Name = x.Name,
                Url = this.Url.Action("Tag", new { name = x.SeoName })
            });
        }

        /// <summary>
        /// Builds the pagination.
        /// </summary>
        /// <param name="pagedPosts">The paged posts.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns>The <see cref="PaginationViewModel" />.</returns>
        private PaginationViewModel BuildPagination(IPagedList<PostThumbViewModel> pagedPosts, string baseUrl)
        {
            var builder = new PaginationBuilder(pagedPosts, MaxPageNumbersToDisplay, new Uri(baseUrl));
            builder.Build();

            return builder.GetResult();
        }
    }
}
