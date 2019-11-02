using System;
using IAmBacon.Core.Application.User.Commands;
using IAmBacon.Core.Infrastructure.User.Fakes;
using IAmBacon.Core.Infrastructure.User.Repositories.Fakes;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.User.Commands
{
    [Subject("User command handler")]
    public class UserCommandHandlerTests
    {
        public class When_repository_argument_null
        {
            Because of = () =>
            {
                _exception = Catch.Exception(() => _sut = new UserCommandHandler(null, null));
            };

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null.");

            static Exception _exception;
            static UserCommandHandler _sut;
        }

        public class When_userManagement_argument_null
        {
            Because of = () =>
            {
                _exception = Catch.Exception(() => _sut = new UserCommandHandler(new UserRepositoryFake(new UserContextFake()), null));
            };

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null.");

            static Exception _exception;
            static UserCommandHandler _sut;
        }
    }
}
