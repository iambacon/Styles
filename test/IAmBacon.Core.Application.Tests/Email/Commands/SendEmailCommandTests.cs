using System;
using IAmBacon.Core.Application.Email.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.Email.Commands
{
    [Subject("Send email command")]
    public class SendEmailCommandTests
    {
        public class When_Name_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new SendEmailCommand(null, null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_set_ParamName = () => ((ArgumentException)_exception).ParamName.ShouldEqual("name");

            static Exception _exception;
            static SendEmailCommand _sut;
        }

        public class When_Email_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new SendEmailCommand("joe bloggs", null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_set_ParamName = () => ((ArgumentException)_exception).ParamName.ShouldEqual("email");

            static Exception _exception;
            static SendEmailCommand _sut;
        }

        public class When_Subject_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new SendEmailCommand("joe bloggs", "joe@bloggs.com", null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_set_ParamName = () => ((ArgumentException)_exception).ParamName.ShouldEqual("subject");

            static Exception _exception;
            static SendEmailCommand _sut;
        }

        public class When_HtmlMessage_null
        {
            Because of = () =>
                _exception = Catch.Exception(() => _sut = new SendEmailCommand("joe bloggs", "joe@bloggs.com", "Reset password", null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_set_ParamName = () => ((ArgumentException)_exception).ParamName.ShouldEqual("htmlMessage");

            static Exception _exception;
            static SendEmailCommand _sut;
        }
    }
}
