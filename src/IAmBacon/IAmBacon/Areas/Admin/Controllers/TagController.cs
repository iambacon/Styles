namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using IAmBacon.Domain.Services.Interfaces;
    using IAmBacon.Model.Common;
    using IAmBacon.Model.Entities;

    /// <summary>
    /// The tag controller.
    /// </summary>
    public class TagController : BaseController
    {
        /// <summary>
        /// The tag service.
        /// </summary>
        private readonly IService<Tag> tagService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TagController"/> class.
        /// </summary>
        /// <param name="tagService">
        /// The tag service.
        /// </param>
        public TagController(IService<Tag> tagService)
        {
            this.tagService = tagService;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag model)
        {
            try
            {
                this.tagService.Create(model);
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
        /// <param name="id">The id.</param>
        /// <param name="form">The form.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Delete(int id, FormCollection form)
        {
            Tag tag = this.tagService.Get(id);
            return this.View(tag);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Tag tag = this.tagService.Get(id);

            IResult result = this.tagService.Delete(tag);

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
            Tag tag = this.tagService.Get(id);
            return this.View(tag);
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
            Tag tag = this.tagService.Get(id);
            return this.View(tag);
        }

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            IResult result = this.tagService.Save(tag);

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
            IEnumerable<Tag> tags = this.tagService.GetAll();
            return this.View(tags);
        }
    }
}