using System;
using IAmBacon.Core.Application.PostCategory.Queries;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.PostCategory.Queries
{
    [Subject("CategoryQueries")]
    public class CategoryQueriesTests
    {
        public class When_connectionString_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new CategoryQueries(null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static Exception _exception;
            static CategoryQueries _sut;
        }
    }
}
