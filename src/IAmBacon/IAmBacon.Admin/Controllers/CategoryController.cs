using System.Threading.Tasks;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Core.Application.PostCategory.Commands;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryCommandHandler _handler;

        public CategoryController(CategoryCommandHandler handler)
        {
            _handler = handler;
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var command = new CreateCategoryCommand(model.Name);

                await _handler.HandleAsync(command);

                return RedirectToAction("Create");
            }
            catch
            {
                return View(model);
            }
        }
    }
}