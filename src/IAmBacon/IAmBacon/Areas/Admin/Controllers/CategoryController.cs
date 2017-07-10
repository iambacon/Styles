namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using IAmBacon.Domain.Services.Interfaces;
    using IAmBacon.Model.Common;
    using IAmBacon.Model.Entities;

    /// <summary>
    /// The category controller.
    /// </summary>
    public class CategoryController : BaseController
    {
        /// <summary>
        /// The category service.
        /// </summary>
        private readonly IService<Category> categoryService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryService">
        /// The category service.
        /// </param>
        public CategoryController(IService<Category> categoryService)
        {
            this.categoryService = categoryService;
        }
        
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
        public ActionResult Create(Category model)
        {
            try
            {
                this.categoryService.Create(model);
                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
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
        public ActionResult Delete(int id)
        {
            var category = this.categoryService.Get(id);
            return this.View(category);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="form">The form.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            var category = this.categoryService.Get(id);
            IResult result = this.categoryService.Delete(category);

            if (result.Success)
            {
                return this.RedirectToAction("Index");
            }

            return this.View();
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
            var category = this.categoryService.Get(id);
            return this.View(category);
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
            var category = this.categoryService.Get(id);
            return this.View(category);
        }

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            IResult result = this.categoryService.Save(category);

            if (result.Success)
            {
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var categories = this.categoryService.GetAll();
            return this.View(categories);
        }
    }
}