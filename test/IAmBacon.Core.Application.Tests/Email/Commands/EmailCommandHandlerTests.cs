using System;
using IAmBacon.Core.Application.Email.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.Email.Commands
{
    [Subject("Email command handler")]
    public class When_email_configuration_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new EmailCommandHandler(null));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

        static Exception _exception;
        static EmailCommandHandler _sut;
    }
}
