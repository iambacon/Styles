using System;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;
using Machine.Specifications;

namespace IAmBacon.Core.Domain.Tests.AggregatesModel.UserAggregate
{
    [Subject("User")]
    public class UserTests
    {
        public class When_argument_first_name_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new User(null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("firstName");

            static User _sut;
            static Exception _exception;
        }

        public class When_argument_last_name_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new User("joe", null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("lastName");

            static User _sut;
            static Exception _exception;
        }

        public class When_argument_email_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new User("joe", "bloggs", null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("email");

            static User _sut;
            static Exception _exception;
        }
    }
}
