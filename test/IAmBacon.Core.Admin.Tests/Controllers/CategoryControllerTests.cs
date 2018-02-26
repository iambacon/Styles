using System;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels;
using IAmBacon.Core.Application.PostCategory.Commands;
using IAmBacon.Core.Application.PostCategory.Queries.Fakes;
using IAmBacon.Core.Infrastructure.PostCategory.Fakes;
using IAmBacon.Core.Infrastructure.PostCategory.Repositories.Fakes;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Category = IAmBacon.Core.Domain.AggregatesModel.PostAggregate.Category;
using It = Machine.Specifications.It;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("Category controller")]
    public class CategoryControllerTests
    {
        public class When_handler_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new CategoryController(null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static CategoryController _sut;
            static Exception _exception;
        }

        public class When_categoryQueries_argument_null
        {
            Establish context = () =>
            {
                var categoryContext = new CategoryContextFake();
                var repo = new CategoryRepositoryFake(categoryContext);
                _handler = new CategoryCommandHandler(repo);
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new CategoryController(_handler, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static CategoryController _sut;
            static Exception _exception;
            static CategoryCommandHandler _handler;
        }
    }

    [Subject("Category controller Create")]
    public class CategoryControllerCreate
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

    [Subject("Category controller Edit")]
    public class CategoryControllerEdit : Category_controller_context
    {
        public class When_get
        {
            Establish context = () =>
            {
                var entity = new Application.PostCategory.Queries.Category
                {
                    Id = 1,
                    Name = "css"
                };

                CategoryQueries.Add(entity);
            };

            Because of = async () => Result = await Sut.Edit(1);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_get_and_category_does_not_exist : Category_controller_context
        {
            Because of = async () => Result = await Sut.Edit(1);

            It should_return_not_found = () => Result.ShouldBeOfExactType<NotFoundResult>();
        }
    }

    public abstract class Category_controller_context
    {
        Establish context = () =>
        {
            var categoryContext = new CategoryContextFake();
            Repo = new CategoryRepositoryFake(categoryContext);
            var handler = new CategoryCommandHandler(Repo);
            CategoryQueries = new CategoryQueriesFake();

            Sut = new CategoryController(handler, CategoryQueries);
        };

        protected static CategoryController Sut;
        protected static IActionResult Result;
        protected static CategoryRepositoryFake Repo;
        protected static CategoryQueriesFake CategoryQueries;
    }
}
