using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using Machine.Specifications;

namespace IAmBacon.Core.Domain.Tests.AggregatesModel.PostAggregate
{
    [Subject("Category")]
    public class CategoryTests
    {
        public class When_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new Category(null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullExeception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static Exception _exception;
            static Category _sut;
        }

        public class SetDeleteStatus
        {
            Establish context = () => _sut = new Category("css");

            Because of = () => _sut.SetDeleteStatus();

            It should_set_active_to_false = () => _sut.IsActive.ShouldBeFalse();

            static Category _sut;
        }
    }
}
