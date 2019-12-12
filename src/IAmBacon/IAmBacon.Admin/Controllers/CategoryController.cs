using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Admin.ViewModels.Category;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Application.PostCategory.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryCommandHandler _handler;
        private readonly ICategoryQueries _categoryQueries;

        public CategoryController(CategoryCommandHandler handler, ICategoryQueries categoryQueries)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryQueries.GetAllAsync();
            return View(categories);
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

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryQueries.GetAsync(id);

                var model = new DeleteCategoryViewModel
                {
                    Id = result.Id,
                    Name = result.Name
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
        public async Task<IActionResult> Delete(DeleteCategoryViewModel model)
        {
            try
            {
                var command = new DeleteCategoryCommand(model.Id);
                await _handler.HandleAsync(command);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _categoryQueries.GetAsync(id);

                // Manual mapping will wait and see how much there is before I do anything
                var model = new EditCategoryViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Active = result.Active,
                    Deleted = result.Deleted
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
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var command = new UpdateCategoryCommand(model.Id, model.Name, model.Active, model.Deleted);
                await _handler.HandleAsync(command);

                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _categoryQueries.GetAsync(id);

                var model = new RetrieveCategoryViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Active = result.Active
                };

                return View(model);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
