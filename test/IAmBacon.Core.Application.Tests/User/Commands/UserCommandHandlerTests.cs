using System;
using IAmBacon.Core.Application.User.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.User.Commands
{
    [Subject("User command handler")]
    public class When_repository_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new UserCommandHandler(null));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

        static Exception _exception;
        static UserCommandHandler _sut;
    }
}
