using System;
using IAmBacon.Core.Application.PostCategory.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.PostCategory.Commands
{
    [Subject("Category command handler")]
    public class When_repository_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new CategoryCommandHandler(null));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

        static Exception _exception;
        static CategoryCommandHandler _sut;
    }
}
