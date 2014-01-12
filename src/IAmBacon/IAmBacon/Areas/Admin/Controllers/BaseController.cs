namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The base controller.
    /// </summary>
    [Authorize]
    public abstract class BaseController : Controller
    {
    }
}