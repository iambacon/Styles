using Autofac;
using IAmBacon.Core.Application.Post.Commands;
using IAmBacon.Core.Application.Post.Queries;

namespace IAmBacon.Core.Application.AutofacModules
{
   public  class PostCommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostCommandHandler>();
            builder.RegisterType<PostQueries>().As<IPostQueries>().InstancePerLifetimeScope();
        }
    }
}
