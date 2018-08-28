using System;
using IAmBacon.Core.Application.Infrastructure;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.Infrastructure
{
    [Subject("Dapper DbConnection Factory")]
    public class When_connectionString_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new DapperDbConnectionFactory(null));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

        static DapperDbConnectionFactory _sut;
        static Exception _exception;
    }
}
