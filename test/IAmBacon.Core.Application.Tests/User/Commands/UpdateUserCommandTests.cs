using System;
using IAmBacon.Core.Application.User.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.User.Commands
{
    [Subject("Update user command")]
    public class UpdateUserCommandTests
    {
        public class When_bio_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new UpdateUserCommand(0, null, null, null, null, null, false, false));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static UpdateUserCommand _sut;
            static Exception _exception;
        }

        public class When_profileImage_null
        {
            Because of = () =>
                _exception = Catch.Exception(() => _sut = new UpdateUserCommand(0, "I am bacon", null, null, null, null, false, false));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static UpdateUserCommand _sut;
            static Exception _exception;
        }

        public class When_firstName_null
        {
            Because of = () =>
                _exception = Catch.Exception(() => _sut = new UpdateUserCommand(0, "I am bacon", "me.jpg", null, null, null, false, false));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static UpdateUserCommand _sut;
            static Exception _exception;
        }

        public class When_lastName_null
        {
            Because of = () =>
                _exception = Catch.Exception(() => _sut = new UpdateUserCommand(0, "I am bacon", "me.jpg", "Joe", null, null, false, false));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static UpdateUserCommand _sut;
            static Exception _exception;
        }

        public class When_email_null
        {
            Because of = () =>
                _exception = Catch.Exception(() => _sut = new UpdateUserCommand(0, "I am bacon", "me.jpg", "Joe", "Bloggs", null, false, false));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static UpdateUserCommand _sut;
            static Exception _exception;
        }
    }
}
