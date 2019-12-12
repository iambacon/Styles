﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAmBacon.Admin.ViewModels.Tag;
using IAmBacon.Core.Application.PostTag.Commands;
using IAmBacon.Core.Application.PostTag.Queries;
using Microsoft.AspNetCore.Http;
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

        public async Task<IActionResult> Index()
        {
            var tags = await _tagQueries.GetAllAsync();

            return View(tags);
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

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _tagQueries.GetAsync(id);

                var model = new EditTagViewModel
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
        public async Task<IActionResult> Edit(EditTagViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var command = new UpdateTagCommand(model.Id, model.Name, model.Active, model.Deleted);
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

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _tagQueries.GetAsync(id);

                var model = new DeleteTagViewModel
                {
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
        public async Task<IActionResult> Delete(DeleteTagViewModel model)
        {
            try
            {
                var command = new DeleteTagCommand(model.Id);
                await _handler.HandleAsync(command);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }
    }
}