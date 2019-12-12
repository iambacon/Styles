using Autofac;
using IAmBacon.Core.Application.Email.Commands;

namespace IAmBacon.Core.Application.AutofacModules
{
    public class EmailCommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailCommandHandler>();
        }
    }
}
