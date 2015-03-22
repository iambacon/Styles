using System;
using System.Web;
using System.Web.Mvc;
using IAmBacon.Attributes;
using IAmBacon.ViewModels;
using IAmBacon.ViewModels.Home;
using Machine.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using It = Machine.Specifications.It;

namespace IAmBacon.Web.Tests.Features
{
    [Subject("Twitter card")]
    public class TwitterCard
    {
        public class When_I_browse_to_a_page
        {
            It should_contain_twitter_metadata;
        }

        public class Given_TwitterMetaTagsAttribute_is_implemented
        {
            static TwitterMetaTagsAttribute _sut;

            static Mock<ActionExecutedContext> _filterContextMock;

            static ITwitterMetadata _expectedMetadata;

            Establish context = () =>
            {
                _expectedMetadata = new HomeViewModel
                {
                    Site = "@iambacon",
                    Url = "http://www.iambacon.co.uk/",
                    Description = "This is the description.",
                    Image = "http://images.iambacon.co.uk/twitter-card.png",
                    HasImage = true
                };

                var httpRequest = new Mock<HttpRequestBase>();
                httpRequest.SetupGet(r => r.Url).Returns(new Uri("http://www.iambacon.co.uk"));

                var httpContext = new Mock<HttpContextBase>();
                httpContext.SetupGet(c => c.Request).Returns(httpRequest.Object);

                _filterContextMock = new Mock<ActionExecutedContext>();
                var viewResultMock = new Mock<ViewResultBase>();

                _filterContextMock.Object.Result = viewResultMock.Object;

                var viewModel = new HomeViewModel { PageTitle = "Colin Bacon - Web Developer" };
                viewResultMock.Object.ViewData.Model = viewModel;

                _filterContextMock.SetupGet(c => c.HttpContext).Returns(httpContext.Object);

                _sut = new TwitterMetaTagsAttribute("This is the description.", "twitter-card.png");
            };

            Because of = () => _sut.OnActionExecuted(_filterContextMock.Object);

            It should_contain_correctly_formatted_metadata = () =>
            {
                var viewResult = _filterContextMock.Object.Result as ViewResultBase;
                Assert.IsNotNull(viewResult);

                var viewModel = viewResult.Model as HomeViewModel;
                Assert.AreSame(viewModel.Title, viewModel.PageTitle);
                Assert.AreSame(viewModel.Site, _expectedMetadata.Site);
                Assert.IsTrue(viewModel.Url.Equals(_expectedMetadata.Url, StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.Description.Equals(_expectedMetadata.Description,
                    StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.Image.Equals(_expectedMetadata.Image,
                    StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.HasImage);
            };
        }
    }
}
