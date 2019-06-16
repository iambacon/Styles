using System;
using System.Linq;
using System.Threading.Tasks;
using IAmBacon.Admin.Presentation.Extensions;
using IAmBacon.Admin.ViewModels.Post;
using IAmBacon.Core.Application.Post.Commands;
using IAmBacon.Core.Application.PostCategory.Queries;
using IAmBacon.Core.Application.PostTag.Queries;
using IAmBacon.Core.Application.User.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly PostCommandHandler _handler;
        private readonly IUserQueries _userQueries;
        private readonly ICategoryQueries _categoryQueries;
        private readonly ITagQueries _tagQueries;

        public PostController(PostCommandHandler handler, IUserQueries userQueries, ICategoryQueries categoryQueries, ITagQueries tagQueries)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
            _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));
            _tagQueries = tagQueries ?? throw new ArgumentNullException(nameof(tagQueries));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var authors = await _userQueries.GetAllAsync();
            var categories = await _categoryQueries.GetAllAsync();
            var tags = await _tagQueries.GetAllAsync();

            var model = new CreatePostViewModel
            {
                Authors = authors.ToSelectList(x => x.ToString(), y => y.Id.ToString()),
                Categories = categories.ToSelectList(x => x.Name, y => y.Id.ToString()),
                Tags = tags.ToCheckboxList(x=> x.Name, y => y.Id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var selectedTags = model.Tags.Where(y => y.IsChecked).Select(x => x.Id).ToArray();

                var command = new CreatePostCommand(model.AuthorId, model.CategoryId, model.Title, model.Markdown)
                {
                    Image = model.Image, TagIds = selectedTags, IsActive = model.Active, NoCss = model.NoCss
                };

                await _handler.HandleAsync(command);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}