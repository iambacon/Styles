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
                var user = await _userQueries.GetAsync(id);

                var model = new DeleteUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.ToString()
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
        public async Task<IActionResult> Delete(DeleteUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var command = new DeleteUserCommand(model.Id, model.Email);
                await _handler.HandleAsync(command);

                return RedirectToAction("Index");
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var user = await _userQueries.GetAsync(id);

                var model = new RetrieveUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Bio = user.Bio,
                    ProfileImage = user.ProfileImage
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