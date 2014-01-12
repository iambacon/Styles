namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using IAmBacon.Areas.Admin.ViewModels;
    using IAmBacon.Domain.Services.Interfaces;

    /// <summary>
    /// The authentication controller.
    /// </summary>
    public class AuthenticationController : BaseController
    {
        #region Fields

        /// <summary>
        /// The membership service.
        /// </summary>
        private readonly IMembershipService membershipService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="membershipService">
        /// The membership service.
        /// </param>
        public AuthenticationController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The Login action method.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;

            return this.View();
        }

        /// <summary>
        /// The httpPost login action method.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                if (this.membershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (this.Url.IsLocalUrl(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }

                    return this.RedirectToAction("Index", "Home");
                }

                this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            }

            return this.View(model);
        }

        /// <summary>
        /// The logout action method.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Login");
        }

        #endregion
    }
}