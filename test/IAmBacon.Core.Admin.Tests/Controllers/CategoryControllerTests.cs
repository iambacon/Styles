using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Infrastructure.PostCategory.Fakes;
using IAmBacon.Core.Infrastructure.PostCategory.Repositories.Fakes;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using It = Machine.Specifications.It;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("Category controller Create")]
    public class CategoryControllerTests
    {
        public class When_get : Category_controller_context
        {
            Because of = () => Result = Sut.Create();

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_post_and_modelState_is_invalid : Category_controller_context
        {
            Establish context = () =>
            {
                Sut.ModelState.AddModelError("Name", "Required");
            };

            Because of = async () => Result = await Sut.Create(new CreateCategoryViewModel());

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_post_and_modelState_is_valid : Category_controller_context
        {
            Because of = async () => Result = await Sut.Create(new CreateCategoryViewModel { Name = "bacon" });

            It should_return_a_redirect = () => Result.ShouldBeOfExactType<RedirectToActionResult>();
        }

        public class When_post_throws_exception : Category_controller_context
        {
            Because of = async () => Result = await Sut.Create(null);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }
    }

    public abstract class Category_controller_context
    {
        Establish context = () =>
        {
            var categoryContext = new CategoryContextFake();
            var repo = new CategoryRepositoryFake(categoryContext);
            var handler = new CategoryCommandHandler(repo);
            Sut = new CategoryController(handler);
        };

        protected static CategoryController Sut;
        protected static IActionResult Result;
    }
}
