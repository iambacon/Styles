using System;
using IAmBacon.Core.Application.PostCategory.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.PostCategory.Commands
{
    [Subject("Update category command")]
    public class When_category_argument_null
    {
        Because of = () => _exception = Catch.Exception(() => _sut = new UpdateCategoryCommand(0, null, true, false));

        It should_throw_an_exception = () => _exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentException>();

        It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

        static Exception _exception;
        static UpdateCategoryCommand _sut;
    }
}
