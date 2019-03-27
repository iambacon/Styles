using System;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using Machine.Specifications;

namespace IAmBacon.Core.Domain.Tests.AggregatesModel.PostAggregate
{
    [Subject("Post")]
    public class PostTests
    {
        public class When_argument_authorId_out_of_range
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new Post(0, 0, null, null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentOutOfRangeException = () => _exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();

            It should_set_ParamName = () => ((ArgumentOutOfRangeException)_exception).ParamName.ShouldEqual("authorId");

            static Exception _exception;
            static Post _sut;
        }

        public class When_categoryId_out_of_range
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new Post(1, 0, null, null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentOutOfRangeException = () => _exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();

            It should_set_ParamName = () => ((ArgumentOutOfRangeException)_exception).ParamName.ShouldEqual("categoryId");

            static Exception _exception;
            static Post _sut;
        }

        public class When_title_out_of_range
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new Post(1, 1, null, null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static Exception _exception;
            static Post _sut;
        }

        public class When_content_out_of_range
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new Post(1, 1, "title", null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static Exception _exception;
            static Post _sut;
        }

        public class When_object_initialised
        {
            Because of = () => _sut = new Post(1, 1, "title", "content");

            It should_set_IsActive_to_false = () => _sut.IsActive.ShouldBeFalse();

            It should_set_new_PostTags = () => _sut.PostTags.ShouldNotBeNull();

            static Post _sut;
        }

        public class SetDelete
        {
            Establish context = () => _sut = new Post(1, 1, "title", "content");

            Because of = () => _sut.SetDelete(true);

            It should_set_IsActive_to_false = () => _sut.IsActive.ShouldBeFalse();

            It should_set_IsDeleted_to_false = () => _sut.Deleted.ShouldBeTrue();

            static Post _sut;
        }

        public class SetActive
        {
            Establish context = () => _sut = new Post(1, 1, "title", "content");

            Because of = () => _sut.SetActive(false);

            It should_set_property = () => _sut.IsActive.ShouldBeFalse();

            static Post _sut;
        }

        public class SetImage_when_argument_null
        {
            Establish context = () => _sut = new Post(1, 1, "title", "content");

            Because of = () => _exception = Catch.Exception(() => _sut.SetImage(null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            static Exception _exception;
            static Post _sut;
        }

        public class AddTag_when_argument_null
        {
            Establish context = () => _sut = new Post(1, 1, "title", "content");

            Because of = () => _exception = Catch.Exception(() => _sut.AddTag(null));

            It should_throw_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static Exception _exception;
            static Post _sut;
        }
    }
}
