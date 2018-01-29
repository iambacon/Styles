using System;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Infrastructure.PostCategory;
using IAmBacon.Core.Infrastructure.PostCategory.Repositories;
using Machine.Specifications;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Admin.IntegrationTests.Controllers
{
    [Subject("Category controller - Create")]
    public class When_adding_a_category
    {
        Establish context = () =>
        {
            _options = new DbContextOptionsBuilder<CategoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        };

        Because of = async () =>
        {
            using (var context = new CategoryContext(_options))
            using (var controller = new CategoryController(new CategoryCommandHandler(new CategoryRepository(context))))
            {
                await controller.Create(new CreateCategoryViewModel { Name = "css" });
            }
        };

        It should_write_to_the_db = () =>
        {
            using (var context = new CategoryContext(_options))
            {
                context.Categories.Count().ShouldEqual(1);
            }
        };

        static DbContextOptions<CategoryContext> _options;
    }

    [Subject("Category controller - Delete")]
    public class When_deleting_a_category
    {
        Establish context = () =>
        {
            _options = new DbContextOptionsBuilder<CategoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            // need to add an item to delete
        };

        Because of = async () =>
        {
            using (var context = new CategoryContext(_options))
            using (var controller = new CategoryController(new CategoryCommandHandler(new CategoryRepository(context))))
            {
                await controller.Delete(new CreateCategoryViewModel { Name = "css" });
            }
        };

        It should_write_to_the_db = () =>
        {
            using (var context = new CategoryContext(_options))
            {
                context.Categories.Count().ShouldEqual(0);
            }
        };

        static DbContextOptions<CategoryContext> _options;
    }
}
