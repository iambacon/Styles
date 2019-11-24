using System;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.User;
using IAmBacon.Core.Application.Infrastructure.Fakes;
using IAmBacon.Core.Application.User.Commands;
using IAmBacon.Core.Application.User.Queries;
using IAmBacon.Core.Infrastructure.Identity;
using IAmBacon.Core.Infrastructure.User;
using IAmBacon.Core.Infrastructure.User.Repositories;
using Machine.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IAmBacon.Core.Admin.IntegrationTests.Controllers
{
    [Subject("User controller - Create")]
    public class When_adding_a_user : User_controller_command_context
    {
        Because of = async () =>
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityContext(IdentityOptions)), null, null,
                null, null, null, null, null, null);

            using (var context = new UserContext(UserOptions))
            using (Sut = new UserController(new UserCommandHandler(new UserRepository(context), userManager), UserQueries))
            {
                await Sut.Create(new CreateUserViewModel
                {
                    Email = "colin@iambacon.co.uk",
                    FirstName = "colin",
                    LastName = "bacon",
                    ProfileImage = "image.jpg",
                    Bio = "I am bacon."
                });
            }
        };

        It should_write_to_the_db = () =>
        {
            using (var context = new UserContext(UserOptions))
            {
                context.Users.Count().ShouldEqual(1);
            }

            using (var context = new IdentityContext(IdentityOptions))
            {
                context.Users.Count().ShouldEqual(1);
            }
        };
    }

    [Subject("User controller - Update")]
    public class When_updating_a_user : User_controller_command_context
    {
        Establish context = () =>
        {
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityContext(IdentityOptions)), null, null,
                null, null, null, null, null, null);

            using (var context = new UserContext(UserOptions))
            {
                context.Users.Add(new Domain.AggregatesModel.UserAggregate.User("Joe", "Bloggs", "joe@bloggs.com", "joe.jpg", "I am Joe."));
                context.SaveChanges();
            }
        };

        Because of = async () =>
        {
            using (var context = new UserContext(UserOptions))
            using (Sut = new UserController(new UserCommandHandler(new UserRepository(context), _userManager), UserQueries))
            {
                await Sut.Edit(new EditUserViewModel
                {
                    Active = false,
                    Bio = "I am Bacon.",
                    Deleted = true,
                    Email = "colin@bacon.com",
                    FirstName = "Colin",
                    Id = 1,
                    LastName = "Bacon",
                    ProfileImage = "me.jpg"
                });
            }
        };

        It should_update_the_user = async () =>
        {
            using (var context = new UserContext(UserOptions))
            {
                var user = await context.Users.FindAsync(1);
                user.ShouldNotBeNull();

                user.IsActive.ShouldBeFalse();
                user.Deleted.ShouldBeTrue();
            }
        };

        static UserManager<IdentityUser> _userManager;
    }

    public abstract class User_controller_command_context
    {
        Establish context = () =>
        {
            var dbName = Guid.NewGuid().ToString();

            UserOptions = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            IdentityOptions = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            UserQueries = new UserQueries(new DapperDbConnectionFactoryFake());
        };

        protected static UserController Sut;
        protected static DbContextOptions<UserContext> UserOptions;
        protected static DbContextOptions<IdentityContext> IdentityOptions;
        protected static IUserQueries UserQueries;
    }
}
