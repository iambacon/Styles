using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
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

    [Subject("Category controller Delete")]
    public class CategoryControllerDelete
    {
        public class When_category_delete_successful : Category_controller_context
        {
            Establish context = () => Repo.Add(new Category("css"));

            Because of = async () => Result = await Sut.Delete(0);

            It should_redirect_to_the_category_page = () => Result.ShouldBeOfExactType<RedirectToActionResult>();
        }

        public class When_category_does_not_exist : Category_controller_context
        {
            Because of = async () => Result = await Sut.Delete(0);

            It should_return_bad_request = () => Result.ShouldBeOfExactType<BadRequestResult>();
        }
    }

    public abstract class Category_controller_context
    {
        Establish context = () =>
        {
            var categoryContext = new CategoryContextFake();
            Repo = new CategoryRepositoryFake(categoryContext);
            var handler = new CategoryCommandHandler(Repo);
            Sut = new CategoryController(handler);
        };

        protected static CategoryController Sut;
        protected static IActionResult Result;
        protected static CategoryRepositoryFake Repo;
    }
}
