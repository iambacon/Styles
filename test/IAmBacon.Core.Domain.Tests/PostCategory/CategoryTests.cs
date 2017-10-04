using System;
using IAmBacon.Core.Domain.PostCategory;
using Machine.Specifications;

namespace IAmBacon.Core.Domain.Tests.PostCategory
{
    [Subject("Category")]
    public class When_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new Category(null));

        It should_throw_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullExeception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

        static Exception _exception;
        static Category _sut;
    }
}
