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

        public class SetDelete
        {
            Establish context = () => _sut = new Tag("css");

            Because of = () => _sut.SetDelete(true);

            It should_set_active_to_false = () => _sut.IsActive.ShouldBeFalse();

            It should_set_IsDeleted_to_false = () => _sut.Deleted.ShouldBeTrue();

            static Tag _sut;
        }

        public class SetName
        {
            Establish context = () => _sut = new Tag("css");

            Because of = () => _sut.SetName("sass");

            It should_set_property = () => _sut.Name.ShouldEqual("sass");

            static Tag _sut;
        }

        public class SetActive
        {
            Establish context = () => _sut = new Tag("css");

            Because of = () => _sut.SetActive(false);

            It should_set_property = () => _sut.IsActive.ShouldBeFalse();

            static Tag _sut;
        }
    }
}
