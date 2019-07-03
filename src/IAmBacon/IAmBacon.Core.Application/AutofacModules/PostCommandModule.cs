using Autofac;
using IAmBacon.Core.Application.Post.Commands;

namespace IAmBacon.Core.Application.AutofacModules
{
   public  class PostCommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostCommandHandler>();
        }
    }
}
