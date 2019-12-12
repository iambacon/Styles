using System;
using IAmBacon.Core.Application.PostTag.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.PostTag.Commands
{
    [Subject("Update tag command")]
    public class When_tag_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new UpdateTagCommand(0, null, true, false));

        It should_throw_an_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentException>();

        It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

        static Exception _exception;
        static UpdateTagCommand _sut;
    }
}
