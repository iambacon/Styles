using Autofac;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Infrastructure.PostTag.Repositories;

namespace IAmBacon.Core.Infrastructure.AutofacModules
{
    public class TagModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TagRepository>().As<ITagRepository>();
        }
    }
}
