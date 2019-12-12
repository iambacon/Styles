using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}