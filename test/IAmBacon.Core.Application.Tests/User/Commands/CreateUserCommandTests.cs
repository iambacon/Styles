using System;
using IAmBacon.Core.Application.User.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.User.Commands
{
    [Subject("Create user command")]
    public class CreateUserCommandTests
    {
        public class When_argument_first_name_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new CreateUserCommand(null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("firstName");

            static CreateUserCommand _sut;
            static Exception _exception;
        }

        public class When_argument_last_name_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new CreateUserCommand("joe", null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("lastName");

            static CreateUserCommand _sut;
            static Exception _exception;
        }

        public class When_argument_email_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new CreateUserCommand("joe", "bloggs", null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("email");

            static CreateUserCommand _sut;
            static Exception _exception;
        }
    }
}
