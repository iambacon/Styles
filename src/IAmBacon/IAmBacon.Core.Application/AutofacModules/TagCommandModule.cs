using Autofac;
using IAmBacon.Core.Application.PostTag.Commands;
using IAmBacon.Core.Application.PostTag.Queries;

namespace IAmBacon.Core.Application.AutofacModules
{
    public class TagCommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TagQueries>().As<ITagQueries>().InstancePerLifetimeScope();
            builder.RegisterType<TagCommandHandler>();
        }
    }
}
