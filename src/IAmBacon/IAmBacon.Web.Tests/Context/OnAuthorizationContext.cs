namespace IAmBacon.Web.Tests.Context
{
    using System.Web;
    using System.Web.Mvc;

    using Machine.Specifications;

    using Moq;

    public class OnAuthorizationContext
    {
        protected static Mock<AuthorizationContext> AuthorizationContextMock;

        protected static Mock<HttpRequestBase> HttpRequestMock;

        protected static Mock<AuthorizationContext> FilterContextMock;

        Establish context = () =>
            {
                HttpRequestMock = new Mock<HttpRequestBase>();

                var httpContextMock = new Mock<HttpContextBase>();
                httpContextMock.SetupGet(c => c.Request).Returns(HttpRequestMock.Object);

                FilterContextMock = new Mock<AuthorizationContext>();
                var viewResultMock = new Mock<ViewResultBase>();

                FilterContextMock.Object.Result = viewResultMock.Object;
                FilterContextMock.SetupGet(c => c.HttpContext).Returns(httpContextMock.Object);
            };
    }
}
