namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ViewModels;
    using Domain.Services.Interfaces;
    using Model.Common;
    using Model.Entities;

    /// <summary>
    /// The admin User controller.
    /// </summary>
    public class UserController : BaseController
    {
        #region Fields

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                // Todo: automapper implementation.
                var user = new User
                    {
                        Active = model.Active,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password
                    };

                var result = this.userService.Create(user);

                if (result.Id != 0)
                {
                    return this.RedirectToAction("Index");
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            User model = this.userService.Get(id);

            return this.View(model);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User model)
        {
            IResult result = this.userService.Delete(model);

            if (result.Success)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        /// <summary>
        /// The details.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(int id)
        {
            User model = this.userService.Get(id);
            return View(model);
        }

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Edit(int id)
        {
            User model = this.userService.Get(id);
            return this.View(model);
        }

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            IResult result = this.userService.Save(model);

            if (result.Success)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            IEnumerable<User> model = this.userService.GetAll();
            return this.View(model);
        }

        #endregion
    }
}