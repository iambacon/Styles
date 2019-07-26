using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAmBacon.Admin.ViewModels.User;
using IAmBacon.Core.Application.User.Commands;
using IAmBacon.Core.Application.User.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly UserCommandHandler _handler;
        private readonly IUserQueries _userQueries;

        public UserController(UserCommandHandler handler, IUserQueries userQueries)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
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
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var command = new CreateUserCommand(model.FirstName, model.LastName, model.Email, model.ProfileImage, model.Bio);
                await _handler.HandleAsync(command);

                return RedirectToAction("Index");
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
                var result = await _userQueries.GetAsync(id);

                var model = new DeleteUserViewModel
                {
                    Name = string.Join(" ", result.FirstName, result.LastName)
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