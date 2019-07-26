using System;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.User;
using IAmBacon.Core.Application.User.Commands;
using IAmBacon.Core.Infrastructure.User;
using IAmBacon.Core.Infrastructure.User.Repositories;
using Machine.Specifications;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Admin.IntegrationTests.Controllers
{
    [Subject("User controller - Create")]
    public class When_adding_a_user : User_controller_command_context
    {
        Because of = async () =>
        {
            using (var context = new UserContext(Options))
            using (Sut = new UserController(new UserCommandHandler(new UserRepository(context))))
            {
                await Sut.Create(new CreateUserViewModel {Email = "colin@iambacon.co.uk", FirstName = "colin", LastName = "bacon", ProfileImage = "image.jpg", Bio = "I am bacon."});
            }
        };

        It should_write_to_the_db = () =>
        {
            using (var context = new UserContext(Options))
            {
                context.Users.Count().ShouldEqual(1);
            }
        };
    }

    public abstract class User_controller_command_context
    {
        Establish context = () =>
        {
            Options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        };

        protected static UserController Sut;
        protected static DbContextOptions<UserContext> Options;
    }
}
