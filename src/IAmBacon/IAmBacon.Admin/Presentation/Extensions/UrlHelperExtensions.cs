using IAmBacon.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Admin.Presentation.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(nameof(AccountController.ResetPassword), "Account", new { userId, code }, scheme);
        }
    }
}
