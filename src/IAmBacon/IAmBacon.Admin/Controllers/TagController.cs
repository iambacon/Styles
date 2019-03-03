using System;
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
    }
}