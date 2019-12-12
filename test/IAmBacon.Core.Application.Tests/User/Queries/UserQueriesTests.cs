using System;
using IAmBacon.Core.Application.User.Queries;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.User.Queries
{
    [Subject("UserQueries")]
    public class UserQueriesTests
    {
        public class When_connectionFactory_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new UserQueries(null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static Exception _exception;
            static UserQueries _sut;
        }
    }
}
