using System;
using System.Threading.Tasks;
using IAmBacon.Admin.ViewModels.User;
using IAmBacon.Core.Application.User.Commands;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly UserCommandHandler _handler;

        public UserController(UserCommandHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var command = new CreateUserCommand(model.FirstName, model.LastName, model.Email);
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