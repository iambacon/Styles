using Autofac;
using IAmBacon.Core.Application.PostCategory.Commands;

namespace IAmBacon.Core.Application.AutofacModules
{
    public class CategoryCommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryCommandHandler>();
        }
    }
}
