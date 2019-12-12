using System;
using IAmBacon.Core.Application.Post.Queries;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.Post.Queries
{
    [Subject("Post queries")]
    public class PostQueriesTests
    {
        public class When_connectionString_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new PostQueries(null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static Exception _exception;
            static PostQueries _sut;
        }
    }
}
