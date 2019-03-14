using System;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.Tag;
using IAmBacon.Core.Application.PostTag.Commands;
using IAmBacon.Core.Application.PostTag.Queries.Fakes;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
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
            Because of = () => _exception = Catch.Exception(() => _sut = new TagController(null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static TagController _sut;
            static Exception _exception;
        }

        public class When_tagQueries_argument_null
        {
            Establish context = () =>
            {
                var tagContext = new TagContextFake();
                var repo = new TagRepositoryFake(tagContext);
                _handler = new TagCommandHandler(repo);
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new TagController(_handler, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static TagController _sut;
            static Exception _exception;
            static TagCommandHandler _handler;
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


    [Subject("Tag controller Edit")]
    public class TagControllerEdit : Tag_controller_context
    {
        public class When_get
        {
            Establish context = () =>
            {
                var entity = new Application.PostTag.Queries.Tag
                {
                    Id = 1,
                    Name = "css"
                };

                TagQueries.Add(entity);
            };

            Because of = async () => Result = await Sut.Edit(1);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_get_and_tag_does_not_exist : Tag_controller_context
        {
            Because of = async () => Result = await Sut.Edit(1);

            It should_return_not_found = () => Result.ShouldBeOfExactType<NotFoundResult>();
        }

        public class When_post_and_modelState_is_invalid : Tag_controller_context
        {
            Establish context = () => Sut.ModelState.AddModelError("Name", "Required");

            Because of = async () => Result = await Sut.Edit(new EditTagViewModel());

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();

            It should_return_modelState_error = () => ((ViewResult)Result).ViewData.ModelState.ErrorCount.ShouldEqual(1);
        }

        public class When_post_and_modelState_is_valid : Tag_controller_context
        {
            Establish context = () => Repo.Add(new Tag("css"));

            Because of = async () => Result = await Sut.Edit(new EditTagViewModel { Name = "css" });

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<RedirectToActionResult>();
        }

        public class When_post_throws_exception : Tag_controller_context
        {
            Because of = async () => Result = await Sut.Edit(new EditTagViewModel { Name = "css" });

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<StatusCodeResult>();

            It should_return_status_code_500 = () => ((StatusCodeResult)Result).StatusCode.ShouldEqual(500);
        }
    }

    [Subject("Tag controller Details")]
    public class TagControllerDetails : Tag_controller_context
    {
        public class When_get
        {
            Establish context = () =>
            {
                var entity = new Application.PostTag.Queries.Tag
                {
                    Id = 1,
                    Name = "css"
                };

                TagQueries.Add(entity);
            };

            Because of = async () => Result = await Sut.Details(1);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_get_and_tag_does_not_exist
        {
            Because of = async () => Result = await Sut.Details(2);

            It should_return_not_found = () => Result.ShouldBeOfExactType<NotFoundResult>();
        }
    }

    [Subject("Tag controller Delete")]
    public class TagControllerDelete : Tag_controller_context
    {
        public class When_get
        {
            Establish context = () =>
            {
                var entity = new Application.PostTag.Queries.Tag
                {
                    Id = 1,
                    Name = "css"
                };

                TagQueries.Add(entity);
            };

            Because of = async () => Result = await Sut.Delete(1);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_get_and_tag_does_not_exist
        {
            Because of = async () => Result = await Sut.Delete(1);

            It should_return_not_found = () => Result.ShouldBeOfExactType<NotFoundResult>();
        }

        public class When_post_and_tag_delete_successful
        {
            Establish context = () => Repo.Add(new Tag("css"));

            Because of = async () => Result = await Sut.Delete(new DeleteTagViewModel { Id = 0, Name = "css" });

            It should_redirect_to_the_tag_page = () => Result.ShouldBeOfExactType<RedirectToActionResult>();
        }

        public class When_post_and_tag_does_not_exist
        {
            Because of = async () => Result = await Sut.Delete(new DeleteTagViewModel { Id = 1, Name = "css" });

            It should_return_bad_request = () => Result.ShouldBeOfExactType<BadRequestResult>();
        }
    }

    public abstract class Tag_controller_context
    {
        Establish context = () =>
        {
            var tagContext = new TagContextFake();
            Repo = new TagRepositoryFake(tagContext);
            var handler = new TagCommandHandler(Repo);
            TagQueries = new TagQueriesFake();

            Sut = new TagController(handler, TagQueries);
        };

        protected static TagController Sut;
        protected static IActionResult Result;
        protected static TagRepositoryFake Repo;
        protected static TagQueriesFake TagQueries;
    }
}
