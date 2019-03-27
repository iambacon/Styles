using System;
using IAmBacon.Core.Application.Post.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.Post.Commands
{
    [Subject("Post command handler")]
    public class When_repository_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new PostCommandHandler(null));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

        static Exception _exception;
        static PostCommandHandler _sut;
    }
}
