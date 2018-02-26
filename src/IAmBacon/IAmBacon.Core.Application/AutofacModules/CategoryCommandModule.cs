using Autofac;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Application.PostCategory.Queries;

namespace IAmBacon.Core.Application.AutofacModules
{
    public class CategoryCommandModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public CategoryCommandModule(string queryString)
        {
            // TODO If the queries connection string is staying change the name of the class
            QueriesConnectionString = queryString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new CategoryQueries(QueriesConnectionString))
                .As<ICategoryQueries>()
                .InstancePerLifetimeScope(); // No idea if this is the correct scope to use.

            builder.RegisterType<CategoryCommandHandler>();
        }
    }
}
