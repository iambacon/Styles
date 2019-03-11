using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAmBacon.Admin.ViewModels.Tag;
using IAmBacon.Core.Application.PostTag.Commands;
using IAmBacon.Core.Application.PostTag.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class TagController : Controller
    {
        private readonly TagCommandHandler _handler;
        private readonly ITagQueries _tagQueries;

        public TagController(TagCommandHandler handler, ITagQueries tagQueries)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _tagQueries = tagQueries ?? throw new ArgumentNullException(nameof(tagQueries));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTagViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var command = new CreateTagCommand(model.Name);
                await _handler.HandleAsync(command);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var result = await _tagQueries.GetAsync(id);

                var model = new RetrieveTagViewModel
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