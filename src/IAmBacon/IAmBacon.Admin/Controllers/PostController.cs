using System;
using System.Threading.Tasks;
using IAmBacon.Admin.Presentation.Extensions;
using IAmBacon.Admin.ViewModels.Post;
using IAmBacon.Core.Application.PostCategory.Queries;
using IAmBacon.Core.Application.PostTag.Queries;
using IAmBacon.Core.Application.User.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly IUserQueries _userQueries;
        private readonly ICategoryQueries _categoryQueries;
        private readonly ITagQueries _tagQueries;

        public PostController(IUserQueries userQueries, ICategoryQueries categoryQueries, ITagQueries tagQueries)
        {
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
    }
}