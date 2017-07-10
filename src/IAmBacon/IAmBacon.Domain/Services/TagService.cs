namespace IAmBacon.Domain.Services
{
    using Data.Infrastructure;

    using Utilities;
    using Model.Common;
    using Model.Entities;

    /// <summary>
    /// The Tag service.
    /// </summary>
    public class TagService : ServiceBase<Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public TagService(IRepository<Tag> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
        
        /// <summary>
        /// The save method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        public override IResult Save(Tag entity)
        {
            entity.SeoName = Seo.SeoUrl(entity.Name);

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
    }
}