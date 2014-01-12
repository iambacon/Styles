namespace IAmBacon.Domain.Services
{
    using Data.Infrastructure;
    using Interfaces;
    using Smtp.Interfaces;
    using Model.Common;
    using Model.Entities;

    /// <summary>
    /// The Comment service.
    /// </summary>
    public class CommentService : ServiceBase<Comment>, ICommentService
    {
        private readonly IEmailManager emailManager;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="emailManager">The email manager.</param>
        public CommentService(IRepository<Comment> repository, IUnitOfWork unitOfWork, IEmailManager emailManager)
            : base(repository, unitOfWork)
        {
            this.emailManager = emailManager;
        }

        /// <summary>
        /// Saves the comment.
        /// </summary>
        /// <param name="entity">The comment.</param>
        /// <returns></returns>
        public override IResult Save(Comment entity)
        {
            if (entity.Id == 0)
            {
                this.Repository.Add(entity);
            }
            else
            {
                this.Repository.Update(entity);
            }

            this.UnitOfWork.Commit();

            // Send email notification to me.
            this.emailManager.SendNewCommentEmail(entity);

            return new Result(true);
        }
    }
}
