namespace IAmBacon.Web.Tests.Attributes
{
    using System;
    using System.Web.Mvc;

    using IAmBacon.Attributes;
    using IAmBacon.Web.Tests.Context;

    using Machine.Specifications;

    public class NoTrailingSlashAttributeTests
    {
        public static NoTrailingSlashAttribute sut;

        [Subject("NoTrailingSlashAttribute")]
        public class When_filterContext_is_null
        {
            static Exception exception;

            Establish context = () => sut = new NoTrailingSlashAttribute();

            Because of = () => exception = Catch.Exception(() => sut.OnAuthorization(null));

            It should_throw_an_exception = () => exception.ShouldNotBeNull();

            It and_should_be_of_type_ArgumentNullException = () => exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        [Subject("NoTrailingSlashAttribute")]
        public class When_trailing_slash : OnAuthorizationContext
        {
            Establish context = () =>
                {
                    HttpRequestMock.SetupGet(r => r.Url).Returns(new Uri("http://www.iambacon.co.uk/"));
                    sut = new NoTrailingSlashAttribute();
                };

            Because of = () => sut.OnAuthorization(FilterContextMock.Object);

            It should_return_404_not_found =
                () => FilterContextMock.Object.Result.ShouldBeOfExactType<HttpNotFoundResult>();
        }
    }
}
