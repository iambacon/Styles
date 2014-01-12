namespace IAmBacon.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using ViewModels;
    using Domain.Services.Interfaces;

    /// <summary>
    ///     The comment controller.
    /// </summary>
    public class CommentController : BaseController
    {
        #region Fields

        /// <summary>
        ///     The comment service.
        /// </summary>
        private readonly ICommentService commentService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommentController" /> class.
        /// </summary>
        /// <param name="commentService">The comment service.</param>
        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Deletes the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var comment = this.commentService.Get(id);

            var model = new CommentViewModel
                {
                    Content = comment.Content,
                    DateCreated = comment.DateCreated,
                    Id = comment.Id,
                    Name = comment.Name,
                    PostTitle = comment.Post.Title,
                    Url = comment.Url
                };

            return this.View(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CommentViewModel model)
        {
            var comment = this.commentService.Get(model.Id);
            this.commentService.Delete(comment);

            return this.RedirectToAction("Index");
        }

        /// <summary>
        ///     The Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // TODO: Automapper.
            IEnumerable<CommentViewModel> comments =
                this.commentService.GetAll()
                    .Select(
                        x =>
                        new CommentViewModel
                            {
                                Content = x.Content,
                                DateCreated = x.DateCreated,
                                Id = x.Id,
                                Name = x.Name,
                                PostTitle = x.Post.Title,
                                Url = x.Url
                            });

            var model = new CommentsViewModel { Comments = comments };

            return View(model);
        }

        #endregion
    }
}