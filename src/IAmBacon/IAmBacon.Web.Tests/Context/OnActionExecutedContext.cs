using System;
using System.Web;
using System.Web.Mvc;
using Machine.Specifications;
using Moq;

namespace IAmBacon.Web.Tests.Context
{
    public abstract class OnActionExecutedContext
    {
        protected static Mock<ActionExecutedContext> FilterContextMock;

        protected static Mock<ViewResultBase> ViewResultMock;

        protected static Mock<HttpRequestBase> HttpRequest;

        Establish context = () =>
        {
            HttpRequest = new Mock<HttpRequestBase>();
            HttpRequest.SetupGet(r => r.Url).Returns(new Uri("https://www.iambacon.co.uk"));

            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(c => c.Request).Returns(HttpRequest.Object);

            FilterContextMock = new Mock<ActionExecutedContext>();
            ViewResultMock = new Mock<ViewResultBase>();

            FilterContextMock.Object.Result = ViewResultMock.Object;

            FilterContextMock.SetupGet(c => c.HttpContext).Returns(httpContext.Object);
        };
    }
}
