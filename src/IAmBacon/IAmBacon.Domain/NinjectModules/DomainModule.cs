namespace IAmBacon.Domain.NinjectModules
{
    using IAmBacon.Model.Entities;

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
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            this.Bind<IPostService>().To<PostService>().InRequestScope();
            this.Bind<IUserService>().To<UserService>().InRequestScope();
            this.Bind<IMembershipService>().To<MembershipService>().InRequestScope();
            this.Bind<IService<Category>>().To<CategoryService>().InRequestScope();
            this.Bind<IService<Tag>>().To<TagService>().InRequestScope();
            this.Bind<IService<Comment>>().To<CommentService>().InRequestScope();
            this.Bind<IEmailManager>().To<EmailManager>().InRequestScope();
        }
    }
}