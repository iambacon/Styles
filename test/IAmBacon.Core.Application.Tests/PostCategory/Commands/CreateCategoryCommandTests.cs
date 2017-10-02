using System;
using IAmBacon.Core.Application.PostCategory.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.PostCategory.Commands
{
    [Subject("Create category command")]
    public class When_no_category_name
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new CreateCategoryCommand(null));

        It should_throw_an_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

        static CreateCategoryCommand _sut;
        static Exception _exception;
    }
}
