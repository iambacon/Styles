using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Infrastructure.PostCategory.Repositories.Fakes;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("Category controller Create")]
    public class When_get
    {
        Establish context = () => 
        {
            var repo = new CategoryRepositoryFake();
            var handler = new CategoryCommandHandler(repo);
            _sut = new CategoryController(handler);
        };

        Because of = () => _result = _sut.Create();

        It should_return_a_view_result = () => _result.ShouldBeOfExactType<ViewResult>();

        static CategoryController _sut;
        static IActionResult _result;
    }

    [Subject("Category controller Create")]
    public class When_post_and_modelState_is_invalid
    {
        Establish context = () =>
        {
            var repo = new CategoryRepositoryFake();
            var handler = new CategoryCommandHandler(repo);
            _sut = new CategoryController(handler);
            _sut.ModelState.AddModelError("Name", "Required");
        };

        Because of = async () => _result = await _sut.Create(new CreateCategoryViewModel());

        It should_return_a_view_result = () => _result.ShouldBeOfExactType<ViewResult>();

        It should_return_a_model_state_error;

        static CategoryController _sut;
        static IActionResult _result;
    }
}
