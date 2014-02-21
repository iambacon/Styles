using System.Linq;
using System.Web.Mvc;
using IAmBacon.Areas.Admin.ViewModels;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Model.Entities;
using IAmBacon.Presentation.Extensions;

namespace IAmBacon.Areas.Admin.Controllers
{
    /// <summary>
    ///     The comment controller.
    /// </summary>
    public class CommentController : BaseController
    {
        private const int TruncateLength = 300;

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
        ///     Deletes the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Comment comment = commentService.Get(id);

            var model = new CommentViewModel
            {
                Content = comment.Content,
                DateCreated = comment.DateCreated,
                Id = comment.Id,
                Name = comment.Name,
                PostTitle = comment.Post.Title,
                Url = comment.Url
            };

            return View(model);
        }

        /// <summary>
        ///     Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CommentViewModel model)
        {
            Comment comment = commentService.Get(model.Id);
            commentService.Delete(comment);

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     The Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            CommentsViewModel model = CommentsViewModel();

            return View(model);
        }

        /// <summary>
        ///     Toggles the active value for the specified comment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Spam(int id)
        {
            Comment comment = commentService.Get(id);

            if (comment == null)
            {
                return View("Index");
            }

            comment.Active = !comment.Active;
            commentService.Save(comment);

            return RedirectToAction("Index");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds the comments view model.
        /// </summary>
        /// <returns>The comments view model.</returns>
        private CommentsViewModel CommentsViewModel()
        {
            // TODO: Automapper.
            var comments =
                commentService.GetAll()
                    .Select(
                        x =>
                            new CommentViewModel
                            {
                                Content = x.Content.TruncateAtWord(TruncateLength),
                                DateCreated = x.DateCreated,
                                Id = x.Id,
                                Name = x.Name,
                                PostTitle = x.Post.Title,
                                Url = x.Url,
                                Active = x.Active
                            }).ToList();

            var spamComments = comments.Where(x => !x.Active)
                .OrderByDescending(x => x.Id)
                .Take(100)
                .ToList();
                

            var model = new CommentsViewModel
            {
                Comments = comments.Where(x => x.Active),
                SpamComments = spamComments
            };

            return model;
        }

        #endregion
    }
}