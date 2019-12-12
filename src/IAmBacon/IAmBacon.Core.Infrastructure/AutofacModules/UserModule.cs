using Autofac;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;
using IAmBacon.Core.Infrastructure.User.Repositories;

namespace IAmBacon.Core.Infrastructure.AutofacModules
{
    public class UserModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
        }
    }
}
