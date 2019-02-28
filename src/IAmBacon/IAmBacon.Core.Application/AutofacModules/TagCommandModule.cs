using Autofac;
using IAmBacon.Core.Application.PostTag.Commands;

namespace IAmBacon.Core.Application.AutofacModules
{
    public class TagCommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TagCommandHandler>();
        }
    }
}
