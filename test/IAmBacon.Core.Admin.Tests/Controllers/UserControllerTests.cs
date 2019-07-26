using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.User;
using IAmBacon.Core.Application.User.Commands;
using IAmBacon.Core.Application.User.Queries.Fakes;
using IAmBacon.Core.Infrastructure.User.Fakes;
using IAmBacon.Core.Infrastructure.User.Repositories.Fakes;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("User controller create")]
    public class UserControllerCreate
    {
        public class When_get : User_controller_context
        {
            Because of = () => Result = Sut.Create();

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_post_and_modelState_is_invalid : User_controller_context
        {
            Establish context = () => Sut.ModelState.AddModelError("Email", "Required");

            Because of = async () => Result = await Sut.Create(new CreateUserViewModel());

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();

            It should_return_a_modelState_error = () => ((ViewResult)Result).ViewData.ModelState.ErrorCount.ShouldEqual(1);
        }

        public class When_post_and_modelState_is_valid : User_controller_context
        {
            Because of = async () => Result = await Sut.Create(new CreateUserViewModel
            { Email = "colin@iambacon.co.uk", FirstName = "Colin", LastName = "Bacon" });

            It should_should_return_a_redirect = () => Result.ShouldBeOfExactType<RedirectToActionResult>();
        }

        public class When_post_throws_exception : User_controller_context
        {
            Because of = async () => Result = await Sut.Create(null);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }
    }

    [Subject("User controller delete")]
    public class UserControllerDelete : User_controller_context
    {
        public class When_get
        {
            Establish context = () =>
            {
                var entity = new Application.User.Queries.User
                {
                    Id = 1,
                    FirstName = "Joe",
                    LastName = "Bloggs"
                };

                UserQueries.Add(entity);
            };

            Because of = async () => Result = await Sut.Delete(1);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_get_and_user_does_not_exist
        {
            Because of = async () => Result = await Sut.Delete(1);

            It should_return_not_found = () => Result.ShouldBeOfExactType<NotFoundResult>();
        }
    }

    public abstract class User_controller_context
    {
        Establish context = () =>
        {
            var userContext = new UserContextFake();
            Repo = new UserRepositoryFake(userContext);
            var handler = new UserCommandHandler(Repo);
            UserQueries = new UserQueriesFake();

            Sut = new UserController(handler, UserQueries);
        };

        protected static UserController Sut;
        protected static IActionResult Result;
        protected static UserRepositoryFake Repo;
        protected static UserQueriesFake UserQueries;
    }
}
