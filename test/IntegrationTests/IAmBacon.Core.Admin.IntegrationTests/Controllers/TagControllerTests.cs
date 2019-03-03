using System;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.Tag;
using IAmBacon.Core.Application.PostTag.Commands;
using IAmBacon.Core.Infrastructure.PostTag;
using IAmBacon.Core.Infrastructure.PostTag.Repositories;
using Machine.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IAmBacon.Core.Admin.IntegrationTests.Controllers
{
    [Subject("Tag controller - Create")]
    public class When_adding_a_tag : Tag_controller_command_context
    {
        Because of = async () =>
        {
            using (var context = new TagContext(Options))
            using (Sut = new TagController(new TagCommandHandler(new TagRepository(context))))
            {
                await Sut.Create(new CreateTagViewModel { Name = "mvc" });
            }
        };

        It should_write_to_the_db = () =>
        {
            using (var context = new TagContext(Options))
            {
                context.Tags.Count().ShouldEqual(1);
            }
        };
    }

    public abstract class Tag_controller_command_context
    {
        Establish context = () =>
        {
            Options = new DbContextOptionsBuilder<TagContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        };

        protected static TagController Sut;
        protected static DbContextOptions<TagContext> Options;
        protected static IConfigurationRoot Config;
    }
}
