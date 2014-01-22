namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using ViewModels;
    using Domain.Services.Interfaces;
    using Model.Entities;
    using Models;
    using Presentation.Extensions;

    /// <summary>
    /// The post controller.
    /// </summary>
    public class PostController : BaseController
    {
        #region Fields

        /// <summary>
        /// The category service.
        /// </summary>
        private readonly ICategoryService categoryService;

        /// <summary>
        ///     The post service.
        /// </summary>
        private readonly IPostService postService;

        /// <summary>
        ///     The tag service.
        /// </summary>
        private readonly ITagService tagService;

        /// <summary>
        ///     The user service.
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PostController" /> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="categoryService">The category service.</param>
        /// <param name="tagService">The tag service.</param>
        public PostController(
            IPostService postService, IUserService userService, ICategoryService categoryService, ITagService tagService)
        {
            this.postService = postService;
            this.userService = userService;
            this.categoryService = categoryService;
            this.tagService = tagService;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create method.
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> authors = this.CreateAuthorsSelectList();
            IEnumerable<SelectListItem> categories = this.CreateCategoriesSelectList();

            // TODO: Create a checkbox list helper method.
            IEnumerable<CheckboxItem> tags = this.CreateTagsSelectList();

            var model = new CreatePostViewModel { Authors = authors, Categories = categories, Tags = tags };
            return this.View(model);
        }

        /// <summary>
        ///     The create.
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CreatePostViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                // Todo: Implement automapper.

                List<int> selectedIds = model.Tags.Where(y => y.IsChecked).Select(x => x.Id).ToList();
                IEnumerable<Tag> selectedTags = this.tagService.Get(x => selectedIds.Contains(x.Id));

                var post = new Post
                    {
                        AuthorId = model.AuthorId,
                        Markdown = model.Markdown,
                        Image = model.Image,
                        Title = model.Title,
                        CategoryId = model.CategoryId,
                        Tags = selectedTags.ToList(),
                        NoCss = model.NoCss,
                        Active = model.Active
                    };

                Post result = this.postService.Create(post);
                if (result.Id > 0)
                {
                    return this.RedirectToAction("Details", new { id = result.Id });
                }
            }

            IEnumerable<SelectListItem> authors = this.CreateAuthorsSelectList();
            IEnumerable<SelectListItem> categories = this.CreateCategoriesSelectList();

            // TODO: Create a checkbox list helper method.
            IEnumerable<CheckboxItem> tags = this.CreateTagsSelectList();

            model.Authors = authors;
            model.Categories = categories;
            model.Tags = tags;

            return this.View(model);
        }

        /// <summary>
        ///     Deletes the post by the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Post model = this.postService.Get(id);
            return this.View(model);
        }

        /// <summary>
        ///     The delete action.
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Post model)
        {
            Post post = this.postService.Get(model.Id);
            this.postService.Delete(post);

            // TODO: some message success to display.
            return this.RedirectToAction("Index");
        }

        /// <summary>
        ///     The details action.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Details(int id)
        {
            Post model = this.postService.Get(id);

            return model == null ? this.View("Index") : this.View(model);
        }

        /// <summary>
        ///     The edit action.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Edit(int id)
        {
            Post post = this.postService.Get(id);

            IEnumerable<SelectListItem> authors = this.CreateAuthorsSelectList();
            IEnumerable<SelectListItem> categories = this.CreateCategoriesSelectList();
            IEnumerable<CheckboxItem> tags = this.CreateTagsSelectList();

            EditPostViewModel model = null;

            if (post != null)
            {
                // TODO: Use automapper.
                // TODO: Bugfix need to add selected tags.
                model = new EditPostViewModel
                    {
                        AuthorId = post.AuthorId,
                        Authors = authors,
                        Categories = categories,
                        CategoryId = post.CategoryId,
                        Id = post.Id,
                        Image = post.Image,
                        Markdown = post.Markdown,
                        Tags = tags,
                        Title = post.Title,
                        DateCreated = post.DateCreated,
                        NoCss = post.NoCss,
                        Active = post.Active
                    };
            }

            return model == null ? this.View("Index") : this.View(model);
        }

        /// <summary>
        ///     Edits the specified model.
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(EditPostViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                List<int> selectedIds = model.Tags.Where(y => y.IsChecked).Select(x => x.Id).ToList();
                IEnumerable<Tag> selectedTags = this.tagService.Get(x => selectedIds.Contains(x.Id));

                var post = new Post
                    {
                        Id = model.Id,
                        AuthorId = model.AuthorId,
                        CategoryId = model.CategoryId,
                        DateCreated = model.DateCreated,
                        Image = model.Image,
                        Markdown = model.Markdown,
                        Tags = selectedTags.ToList(),
                        Title = model.Title,
                        NoCss = model.NoCss,
                        Active = model.Active
                    };
                this.postService.Save(post);
                return this.RedirectToAction("Index");
            }

            IEnumerable<SelectListItem> authors = this.CreateAuthorsSelectList();
            IEnumerable<SelectListItem> categories = this.CreateCategoriesSelectList();
            IEnumerable<CheckboxItem> tags = this.CreateTagsSelectList();

            model.Authors = authors;
            model.Categories = categories;
            model.Tags = tags;

            return this.View(model);
        }

        /// <summary>
        ///     The index action.
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            // TODO: automapper
            var postsModel =
                this.postService.GetAll()
                    .Select(
                        x =>
                        new PostViewModel
                            {
                                Author = x.User.FirstName,
                                CategoryName = x.Category.Name,
                                DateCreated = x.DateCreated,
                                DateModified = x.DateModified,
                                Id = x.Id,
                                Summary = x.Content.GetFirstParagraph().ToString(),
                                Tags = x.Tags,
                                Title = x.Title
                            });

            var model = new PostsViewModel
                {
                    Posts = postsModel
                };

            return this.View(model);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the authors select list.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> CreateAuthorsSelectList()
        {
            var authors = this.userService.GetAll().ToSelectList(
                x => x.FirstName,
                y => y.Id.ToString(CultureInfo.InvariantCulture));

            return authors;
        }

        /// <summary>
        /// Creates the categories select list.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> CreateCategoriesSelectList()
        {
            var categories = this.categoryService.GetAll().ToSelectList(
                x => x.Name,
                y => y.Id.ToString(CultureInfo.InvariantCulture));

            return categories;
        }

        /// <summary>
        /// Creates the tags select list.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CheckboxItem> CreateTagsSelectList()
        {
            IEnumerable<CheckboxItem> tags =
                this.tagService.GetAll().Select(x => new CheckboxItem { IsChecked = false, Label = x.Name, Id = x.Id });

            return tags;
        }

        #endregion
    }
}