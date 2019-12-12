using Autofac;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Infrastructure.PostCategory.Repositories;

namespace IAmBacon.Core.Infrastructure.AutofacModules
{
    public class CategoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
        }
    }
}
