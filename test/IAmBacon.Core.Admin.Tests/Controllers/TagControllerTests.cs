using System;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.Tag;
using IAmBacon.Core.Application.PostTag.Commands;
using IAmBacon.Core.Infrastructure.PostTag.Fakes;
using IAmBacon.Core.Infrastructure.PostTag.Repositories.Fakes;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("Tag controller")]
    public class TagControllerTests
    {
        public class When_handler_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new TagController(null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static TagController _sut;
            static Exception _exception;
        }
    }

    [Subject("Tag controller Create")]
    public class TagControllerCreate
    {
        public class When_get : Tag_controller_context
        {
            Because of = () => Result = Sut.Create();

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_post_and_modelState_is_invalid : Tag_controller_context
        {
            Establish context = () =>
            {
                Sut.ModelState.AddModelError("Name", "Required");
            };

            Because of = async () => Result = await Sut.Create(new CreateTagViewModel { Name = "mvc" });

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();

            It should_return_modelState_error = () => ((ViewResult)Result).ViewData.ModelState.ErrorCount.ShouldEqual(1);
        }

        public class When_post_and_modelState_is_valid : Tag_controller_context
        {
            Because of = async () => Result = await Sut.Create(null);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }
    }

    public abstract class Tag_controller_context
    {
        Establish context = () =>
        {
            var tagContext = new TagContextFake();
            Repo = new TagRepositoryFake(tagContext);
            var handler = new TagCommandHandler(Repo);

            Sut = new TagController(handler);
        };

        protected static TagController Sut;
        protected static IActionResult Result;
        protected static TagRepositoryFake Repo;
    }
}
