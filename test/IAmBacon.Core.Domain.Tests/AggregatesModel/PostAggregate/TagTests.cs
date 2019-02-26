using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using Machine.Specifications;

namespace IAmBacon.Core.Domain.Tests.AggregatesModel.PostAggregate
{
    [Subject("Tag")]
    public class TagTests
    {
        public class When_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new Tag(null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullExeception = () => _exception.ShouldBeOfExactType<ArgumentException>();

            static Exception _exception;
            static Tag _sut;
        }
    }
}
