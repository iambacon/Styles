namespace IAmBacon.Domain.NinjectModules
{
    using Services;
    using Services.Interfaces;
    using Smtp;
    using Smtp.Interfaces;

    using Ninject.Modules;
    using Ninject.Web.Common;

    /// <summary>
    /// The domain module.
    /// </summary>
    public class DomainModule : NinjectModule
    {
        #region Public Methods and Operators

        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            this.Bind<IPostService>().To<PostService>().InRequestScope();
            this.Bind<IUserService>().To<UserService>().InRequestScope();
            this.Bind<IMembershipService>().To<MembershipService>().InRequestScope();
            this.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            this.Bind<ITagService>().To<TagService>().InRequestScope();
            this.Bind<ICommentService>().To<CommentService>().InRequestScope();
            this.Bind<IEmailManager>().To<EmailManager>().InRequestScope();
        }

        #endregion
    }
}