using System;
using IAmBacon.Core.Application.PostTag.Queries;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.PostTag.Queries
{
    [Subject("TagQueries")]
    public class TagQueriesTests
    {
        public class When_connectionString_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new TagQueries(null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static Exception _exception;
            static TagQueries _sut;
        }
    }
}
