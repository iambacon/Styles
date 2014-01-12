namespace IAmBacon.Domain.Services
{
    using Data.Infrastructure;
    using Interfaces;
    using Utilities;
    using Model.Common;
    using Model.Entities;

    /// <summary>
    /// The Category service.
    /// </summary>
    public class CategoryService : ServiceBase<Category>, ICategoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CategoryService(IRepository<Category> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        #region Implementation of IService<Category>

        /// <summary>
        /// The save method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        public override IResult Save(Category entity)
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

        #endregion
    }
}
