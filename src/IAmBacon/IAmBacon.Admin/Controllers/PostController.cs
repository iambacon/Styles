using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAmBacon.Admin.Presentation.Extensions;
using IAmBacon.Admin.Presentation.Models;
using IAmBacon.Admin.ViewModels.Post;
using IAmBacon.Core.Application.Post.Commands;
using IAmBacon.Core.Application.Post.Queries;
using IAmBacon.Core.Application.PostCategory.Queries;
using IAmBacon.Core.Application.PostTag.Queries;
using IAmBacon.Core.Application.User.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IAmBacon.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly PostCommandHandler _handler;
        private readonly IUserQueries _userQueries;
        private readonly ICategoryQueries _categoryQueries;
        private readonly ITagQueries _tagQueries;
        private readonly IPostQueries _postQueries;

        public PostController(PostCommandHandler handler, IUserQueries userQueries, ICategoryQueries categoryQueries,
            ITagQueries tagQueries, IPostQueries postQueries)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
            _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));
            _tagQueries = tagQueries ?? throw new ArgumentNullException(nameof(tagQueries));
            _postQueries = postQueries ?? throw new ArgumentNullException(nameof(postQueries));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreatePostViewModel
            {
                Authors = await GetAuthors(),
                Categories = await GetCategories(),
                Tags = await GetTags()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await GetAuthors();
                model.Categories = await GetCategories();

                return View(model);
            }

            try
            {
                var selectedTags = model.Tags.Where(y => y.IsChecked).Select(x => x.Id).ToArray();

                var command = new CreatePostCommand(model.AuthorId, model.CategoryId, model.Title, model.Markdown)
                {
                    Image = model.Image,
                    TagIds = selectedTags,
                    IsActive = model.Active,
                    NoCss = model.NoCss
                };

                await _handler.HandleAsync(command);

                return RedirectToAction("Index");
            }
            catch
            {
                model.Authors = await GetAuthors();
                model.Categories = await GetCategories();

                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _postQueries.GetAsync(id);
                var tags = await SetSelectedTags(id);

                var model = new EditPostViewModel
                {
                    PostId = id,
                    Active = result.Active,
                    AuthorId = result.AuthorId,
                    Authors = await GetAuthors(),
                    Categories = await GetCategories(),
                    CategoryId = result.CategoryId,
                    Image = result.Image,
                    Markdown = result.Markdown,
                    NoCss = result.NoCss,
                    Tags = tags,
                    Title = result.Title
                };

                return View(model);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await GetAuthors();
                model.Categories = await GetCategories();

                return View(model);
            }

            try
            {
                var selectedTags = model.Tags.Where(y => y.IsChecked).Select(x => x.Id).ToArray();

                var command = new UpdatePostCommand(model.PostId, model.AuthorId, model.CategoryId, model.Title, model.Markdown)
                {
                    Image = model.Image,
                    TagIds = selectedTags,
                    IsActive = model.Active,
                    NoCss = model.NoCss
                };

                await _handler.HandleAsync(command);

                return RedirectToAction("Index");
            }
            catch
            {
                model.Authors = await GetAuthors();
                model.Categories = await GetCategories();

                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _postQueries.GetAsync(id);

                var model = new DeletePostViewModel
                {
                    Id = result.Id,
                    Title = result.Title
                };

                return View(model);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeletePostViewModel model)
        {
            try
            {
                var command = new DeletePostCommand(model.Id);
                await _handler.HandleAsync(command);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        private async Task<List<SelectListItem>> GetCategories()
        {
            var categories = await _categoryQueries.GetAllAsync();
            return categories.ToSelectList(x => x.Name, y => y.Id.ToString());
        }

        private async Task<List<SelectListItem>> GetAuthors()
        {
            var authors = await _userQueries.GetAllAsync();
            return authors.ToSelectList(x => x.ToString(), y => y.Id.ToString());
        }

        private async Task<List<CheckboxItem>> GetTags()
        {
            var tags = await _tagQueries.GetAllAsync();

            return tags.ToCheckboxList(x => x.Name, y => y.Id);
        }

        private async Task<List<CheckboxItem>> SetSelectedTags(int id)
        {
            var postTags = await _tagQueries.GetTagsForPost(id);
            var tags = await GetTags();

            foreach (var postTag in postTags)
            {
                var tag = tags.FirstOrDefault(x => x.Id == postTag.Id);

                if (tag != null)
                    tag.IsChecked = true;
            }

            return tags;
        }
    }
}