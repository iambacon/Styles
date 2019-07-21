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
            Because of = () => _exception = Catch.Exception(() => _sut = new User(null, null, null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("firstName");

            static User _sut;
            static Exception _exception;
        }

        public class When_argument_last_name_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new User("joe", null, null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("lastName");

            static User _sut;
            static Exception _exception;
        }

        public class When_argument_email_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new User("joe", "bloggs", null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("email");

            static User _sut;
            static Exception _exception;
        }

        public class When_argument_profileImage_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new User("joe", "bloggs", "joe@bloggs.com", null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("profileImage");

            static User _sut;
            static Exception _exception;
        }

        public class When_argument_bio_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new User("joe", "bloggs", "joe@bloggs.com", "image.png", null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            It should_contain_the_param_name = () => ((ArgumentException)_exception).ParamName.ShouldEqual("bio");

            static User _sut;
            static Exception _exception;
        }

        public class When_initialised
        {
            Because of = () => _sut = new User("joe", "bloggs", "joe@bloggs.com", "image.png", "I am Bacon.");

            It should_set_active_true = () => _sut.IsActive.ShouldBeTrue();

            static User _sut;
        }

        public class SetDelete
        {
            Establish context = () => _sut = new User("joe", "bloggs", "joe@bloggs.com", "image.png", "I am Bacon.");

            Because of = () => _sut.SetDelete(true);

            It should_set_active_to_false = () => _sut.IsActive.ShouldBeFalse();

            It should_set_IsDeleted_to_false = () => _sut.Deleted.ShouldBeTrue();

            static User _sut;
        }

        public class SetActive
        {
            Establish context = () => _sut = new User("joe", "bloggs", "joe@bloggs.com", "image.png", "I am Bacon.");

            Because of = () => _sut.SetActive(false);

            It should_set_property = () => _sut.IsActive.ShouldBeFalse();

            static User _sut;
        }
    }
}
