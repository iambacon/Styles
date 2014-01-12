namespace IAmBacon.Data.NinjectModules
{
    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Data.Repositories;
    using IAmBacon.Model.Entities;

    using Ninject.Modules;
    using Ninject.Web.Common;

    /// <summary>
    /// The data module.
    /// </summary>
    public class DataModule : NinjectModule
    {
        #region Public Methods and Operators

        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            this.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();

            // Todo: there is a more concise way of doing this.
            ////this.Bind(typeof(IRepository<>)).To(typeof(SqlRepositoryBase<>)).InRequestScope();
            this.Bind<IRepository<Post>>().To<SqlPostsRepository>().InRequestScope();
            this.Bind<IRepository<User>>().To<SqlUsersRepository>().InRequestScope();
            this.Bind<IRepository<Category>>().To<SqlCategoriesRepository>().InRequestScope();
            this.Bind<IRepository<Tag>>().To<SqlTagsRepository>().InRequestScope();
            this.Bind<IRepository<Comment>>().To<SqlCommentsRepository>().InRequestScope();
        }

        #endregion
    }
}