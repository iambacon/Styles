using System;
using IAmBacon.Core.Application.Post.Commands;
using Machine.Specifications;

namespace IAmBacon.Core.Application.Tests.Post.Commands
{
    [Subject("Update post command")]
    public class UpdatePostCommandTests
    {
        public class When_authorId_out_of_range
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new UpdatePostCommand(0, 0, 0, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentOutOfRangeException = () => _exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();

            It should_set_ParamName = () => ((ArgumentOutOfRangeException) _exception).ParamName.ShouldEqual("authorId");

            static UpdatePostCommand _sut;
            static Exception _exception;
        }

        public class When_categoryId_out_of_range
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new UpdatePostCommand(0, 1, 0, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentOutOfRangeException = () => _exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();

            It should_set_ParamName = () => ((ArgumentOutOfRangeException) _exception).ParamName.ShouldEqual("categoryId");

            static UpdatePostCommand _sut;
            static Exception _exception;
        }

        public class When_title_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new UpdatePostCommand(0, 1, 1, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static UpdatePostCommand _sut;
            static Exception _exception;
        }

        public class When_content_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new UpdatePostCommand(0, 1, 1, "title", null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentException = () => _exception.ShouldBeOfExactType<ArgumentException>();

            It should_contain_an_error_message = () => _exception.ShouldContainErrorMessage("Value cannot be null or whitespace.");

            static UpdatePostCommand _sut;
            static Exception _exception;
        }
    }
}
