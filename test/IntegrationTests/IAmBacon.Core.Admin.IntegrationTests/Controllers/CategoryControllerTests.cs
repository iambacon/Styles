using System;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Admin.ViewModels.Category;
using IAmBacon.Core.Application.Infrastructure.Fakes;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Application.PostCategory.Queries;
using IAmBacon.Core.Infrastructure.PostCategory;
using IAmBacon.Core.Infrastructure.PostCategory.Repositories;
using Machine.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Category = IAmBacon.Core.Domain.AggregatesModel.PostAggregate.Category;

namespace IAmBacon.Core.Admin.IntegrationTests.Controllers
{
    [Subject("Category controller - Create")]
    public class When_adding_a_category : Category_controller_command_context
    {
        Because of = async () =>
        {
            using (var context = new CategoryContext(Options))
            using (Sut = new CategoryController(new CategoryCommandHandler(new CategoryRepository(context)), CategoryQueries))
            {
                await Sut.Create(new CreateCategoryViewModel { Name = "css" });
            }
        };

        It should_write_to_the_db = () =>
        {
            using (var context = new CategoryContext(Options))
            {
                context.Categories.Count().ShouldEqual(1);
            }
        };
    }

    [Subject("Category controller - Delete")]
    public class When_deleting_a_category : Category_controller_command_context
    {
        Establish context = () =>
        {
            // need to add an item to delete
            using (var context = new CategoryContext(Options))
            {
                context.Categories.Add(new Category("css"));
                context.SaveChanges();
            }
        };

        Because of = async () =>
        {
            using (var context = new CategoryContext(Options))
            using (Sut = new CategoryController(new CategoryCommandHandler(new CategoryRepository(context)), CategoryQueries))
            {
                await Sut.Delete(1);
            }
        };

        It should_set_the_entity_to_inactive = () =>
        {
            using (var context = new CategoryContext(Options))
            {
                var category = context.Categories.IgnoreQueryFilters().First(x => x.Name == "css");
                category.IsActive.ShouldBeFalse();
                category.Deleted.ShouldBeTrue();
            }
        };
    }

    [Subject("Category controller - Update")]
    public class When_updating_a_category : Category_controller_command_context
    {
        Establish context = () =>
        {
            using (var context = new CategoryContext(Options))
            {
                context.Categories.Add(new Category("css"));
                context.SaveChanges();
            }
        };

        Because of = async () =>
        {
            using (var context = new CategoryContext(Options))
            using (Sut = new CategoryController(new CategoryCommandHandler(new CategoryRepository(context)), CategoryQueries))
            {
                await Sut.Edit(new EditCategoryViewModel { Name = "sass" });
            }
        };

        It should_update_the_category_name = async () =>
        {
            using (var context = new CategoryContext(Options))
            {
                var category = await context.Categories.FindAsync(1);
                category.ShouldNotBeNull();
                category.Name.ShouldBeTheSameAs("sass");
            }
        };
    }

    public abstract class Category_controller_command_context
    {
        Establish context = () =>
        {
            Options = new DbContextOptionsBuilder<CategoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            CategoryQueries = new CategoryQueries(new DapperDbConnectionFactoryFake());
        };

        protected static CategoryController Sut;
        protected static DbContextOptions<CategoryContext> Options;
        protected static IConfigurationRoot Config;
        protected static ICategoryQueries CategoryQueries;
    }
}
