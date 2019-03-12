using System;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.Tag;
using IAmBacon.Core.Application.Infrastructure.Fakes;
using IAmBacon.Core.Application.PostTag.Commands;
using IAmBacon.Core.Application.PostTag.Queries;
using IAmBacon.Core.Infrastructure.PostTag;
using IAmBacon.Core.Infrastructure.PostTag.Repositories;
using Machine.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tag = IAmBacon.Core.Domain.AggregatesModel.PostAggregate.Tag;

namespace IAmBacon.Core.Admin.IntegrationTests.Controllers
{
    [Subject("Tag controller - Create")]
    public class When_adding_a_tag : Tag_controller_command_context
    {
        Because of = async () =>
        {
            using (var context = new TagContext(Options))
            using (Sut = new TagController(new TagCommandHandler(new TagRepository(context)), TagQueries))
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

    [Subject("Tag controller - Update")]
    public class When_updating_a_tag : Tag_controller_command_context
    {
        Establish context = () =>
        {
            using (var context = new TagContext(Options))
            {
                context.Tags.Add(new Tag("css"));
                context.SaveChanges();
            }
        };

        Because of = async () =>
        {
            using (var context = new TagContext(Options))
            using (Sut = new TagController(new TagCommandHandler(new TagRepository(context)), TagQueries))
            {
                await Sut.Edit(new EditTagViewModel { Name = "sass" });
            }
        };

        It should_update_the_tag_name = async () =>
        {
            using (var context = new TagContext(Options))
            {
                var tag = await context.Tags.FindAsync(1);
                tag.ShouldNotBeNull();
                tag.Name.ShouldBeTheSameAs("sass");
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

            TagQueries = new TagQueries(new DapperDbConnectionFactoryFake());
        };

        protected static TagController Sut;
        protected static DbContextOptions<TagContext> Options;
        protected static IConfigurationRoot Config;
        protected static ITagQueries TagQueries;
    }
}
