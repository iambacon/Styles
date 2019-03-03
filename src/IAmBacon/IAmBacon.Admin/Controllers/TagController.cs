using System;
using System.Threading.Tasks;
using IAmBacon.Admin.ViewModels.Tag;
using IAmBacon.Core.Application.PostTag.Commands;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class TagController : Controller
    {
        private readonly TagCommandHandler _handler;

        public TagController(TagCommandHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
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
    }
}