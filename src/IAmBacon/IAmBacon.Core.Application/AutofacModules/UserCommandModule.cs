using Autofac;
using IAmBacon.Core.Application.User.Queries;

namespace IAmBacon.Core.Application.AutofacModules
{
    public class UserCommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserQueries>().As<IUserQueries>().InstancePerLifetimeScope();
        }
    }
}
