using Autofac;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Infrastructure.Post.Repositories;

namespace IAmBacon.Core.Infrastructure.AutofacModules
{
    public class PostModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostRepository>().As<IPostRepository>();
        }
    }
}
