using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Core.Admin.Tests.Stubs;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("Category controller Create")]
    public class When_get
    {
        Establish context = () =>
        {
            _fixture = AutoFixtureFactory.CreateOmitOnRecursionFixture().Customize(new AutoConfiguredMoqCustomization());
            _sut = _fixture.Create<CategoryController>();
        };

        Because of = () => _result = _sut.Create();

        It should_return_a_view_result = () => _result.ShouldBeOfExactType<ViewResult>();

        static CategoryController _sut;
        static IActionResult _result;
        static IFixture _fixture;
    }

    [Subject("Category controller Create")]
    public class When_post_and_modelState_is_invalid
    {
        Establish context = () =>
        {
            _fixture = AutoFixtureFactory.CreateOmitOnRecursionFixture().Customize(new AutoConfiguredMoqCustomization());
            _sut = _fixture.Create<CategoryController>();
            _sut.ModelState.AddModelError("Name", "Required");
        };

        Because of = async () => _result = await _sut.Create(new CreateCategoryViewModel());

        It should_return_a_view_result = () => _result.ShouldBeOfExactType<ViewResult>();

        It should_return_a_model_state_error = () => ()

        static CategoryController _sut;
        static IActionResult _result;
        static IFixture _fixture;
    }
}
