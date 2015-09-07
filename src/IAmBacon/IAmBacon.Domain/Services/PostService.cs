namespace IAmBacon.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Infrastructure;
    using Interfaces;
    using Utilities;
    using Model.Common;
    using Model.Entities;

    using MarkdownSharp;

    /// <summary>
    /// The post service.
    /// </summary>
    public class PostService : ServiceBase<Post>, IPostService
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="postRepository">
        /// The post repository.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public PostService(IRepository<Post> postRepository, IUnitOfWork unitOfWork)
            : base(postRepository, unitOfWork)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        public override IResult Save(Post entity)
        {
            entity.Content = TransformMarkdown(entity.Markdown);
            entity.SeoTitle = Seo.SeoUrl(entity.Title);

            if (entity.Id == 0)
            {
                this.Repository.Add(entity);
            }
            else
            {
                this.Repository.Update(entity);
            }

            this.UnitOfWork.Commit();

            return new Result(true);
        }

        /// <summary>
        /// Gets the post by the specified seo title.
        /// </summary>
        /// <param name="seoTitle">The seo title.</param>
        /// <returns>The post object.</returns>
        public Post Get(string seoTitle)
        {
            // TODO: this should call Get.
            return this.Repository.Find(x => x.SeoTitle == seoTitle).FirstOrDefault();
        }

        /// <summary>
        /// Gets the latest active posts.
        /// </summary>
        /// <param name="number">The number of posts to return.</param>
        /// <returns>
        /// Returns the specified number of posts, ordered descending by id.
        /// </returns>
        public IEnumerable<Post> GetLatest(int number)
        {
            return this.GetAll()
                .Where(x => x.Active)
                .OrderByDescending(x => x.Id)
                .Take(number);
        }

        /// <summary>
        /// Gets the popular active posts.
        /// </summary>
        /// <param name="number">The number of posts to return.</param>
        /// <returns>
        /// Returns the specified number of posts.
        /// </returns>
        public IEnumerable<Post> GetPopular(int number)
        {
            var postIdList = new List<int> { 15, 18, 10 };

            return this.Get(x => postIdList.Contains(x.Id));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Transforms the markdown.
        /// </summary>
        /// <param name="markdownText">The markdown text.</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string TransformMarkdown(string markdownText)
        {
            // Todo: need to abstract concrete implementation of MarkdownSharp into a testable service.
            var markdown = new Markdown();
            return markdown.Transform(markdownText);
        }

        #endregion
    }
}