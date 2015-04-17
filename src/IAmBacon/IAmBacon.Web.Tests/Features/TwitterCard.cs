using System;
using System.Web;
using System.Web.Mvc;
using IAmBacon.Attributes;
using IAmBacon.ViewModels;
using IAmBacon.ViewModels.Home;
using IAmBacon.Web.Tests.Context;
using Machine.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using It = Machine.Specifications.It;

namespace IAmBacon.Web.Tests.Features
{
    [Subject("Twitter card")]
    public class TwitterCard
    {
        public class Given_TwitterMetaTagsAttribute_is_implemented : OnActionExecutedContext
        {
            static TwitterMetaTagsAttribute _sut;

            static ITwitterMetadata _expectedMetadata;

            Establish context = () =>
            {
                _expectedMetadata = new HomeViewModel
                {
                    Site = "@iambacon",
                    Url = "http://www.iambacon.co.uk/",
                    Description = "This is the description.",
                    Image = "http://images.iambacon.co.uk/blog/twitter-card.png",
                    HasImage = true
                };

                var viewModel = new HomeViewModel { PageTitle = "Colin Bacon - Web Developer" };
                ViewResultMock.Object.ViewData.Model = viewModel;

                _sut = new TwitterMetaTagsAttribute("This is the description.", "twitter-card.png");
            };

            Because of = () => _sut.OnActionExecuted(FilterContextMock.Object);

            It should_contain_correctly_formatted_metadata = () =>
            {
                var viewResult = FilterContextMock.Object.Result as ViewResultBase;
                Assert.IsNotNull(viewResult);

                var viewModel = viewResult.Model as HomeViewModel;
                Assert.AreSame(viewModel.MetaTitle, viewModel.PageTitle);
                Assert.AreSame(viewModel.Site, _expectedMetadata.Site);
                Assert.IsTrue(viewModel.Url.Equals(_expectedMetadata.Url, StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.Description.Equals(_expectedMetadata.Description,
                    StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.Image.Equals(_expectedMetadata.Image,
                    StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.HasImage);
            };
        }

        public class TwitterMetaTagsAttribute_image : OnActionExecutedContext
        {
            static TwitterMetaTagsAttribute _sut;

            static ITwitterMetadata _expectedMetadata;

            Establish context = () =>
            {
                _expectedMetadata = new HomeViewModel
                {
                    Image = null
                };

                var viewModel = new HomeViewModel { PageTitle = "Colin Bacon - Web Developer" };
                ViewResultMock.Object.ViewData.Model = viewModel;

                _sut = new TwitterMetaTagsAttribute("This is the description.");
            };

            Because of = () => _sut.OnActionExecuted(FilterContextMock.Object);

            It should_allow_the_image_to_be_optional = () =>
            {
                var viewResult = FilterContextMock.Object.Result as ViewResultBase;
                viewResult.ShouldNotBeNull();

                var viewModel = viewResult.Model as HomeViewModel;
                viewModel.Image.ShouldBeNull();
            };
        }

        public class TwitterMetaTagsAttribute_url : OnActionExecutedContext
        {
            static TwitterMetaTagsAttribute _sut;

            static ITwitterMetadata _expectedMetadata;

            Establish context = () =>
            {
                _expectedMetadata = new HomeViewModel
                {
                    Url = "http://www.iambacon.co.uk/"
                };

                HttpRequest.SetupGet(r => r.Url).Returns(new Uri("http://www.iambacon.co.uk?query=param"));

                var viewModel = new HomeViewModel { PageTitle = "Colin Bacon - Web Developer" };
                ViewResultMock.Object.ViewData.Model = viewModel;

                _sut = new TwitterMetaTagsAttribute("This is the description.", "twitter-card.png");
            };

            Because of = () => _sut.OnActionExecuted(FilterContextMock.Object);

            It should_return_the_canonical_url = () =>
            {
                var viewResult = FilterContextMock.Object.Result as ViewResultBase;
                viewResult.ShouldNotBeNull();

                var viewModel = viewResult.Model as HomeViewModel;
                Assert.IsTrue(viewModel.Url.Equals(_expectedMetadata.Url, StringComparison.OrdinalIgnoreCase));
            };
        }

        public class Given_PostTwitterMetaTagsAttribute : OnActionExecutedContext
        {
            static PostTwitterMetaTagsAttribute _sut;

            static ITwitterMetadata _expectedMetadata;

            private Establish context = () =>
            {
                _expectedMetadata = new PostViewModel
                {
                    Site = "@iambacon",
                    Url = "http://www.iambacon.co.uk/",
                    Description = "First paragraph",
                    Image = "http://images.iambacon.co.uk/blog/test.png",
                    HasImage = true
                };

                var viewModel = new PostViewModel
                {
                    PageTitle = "Colin Bacon - Web Developer",
                    Content = new HtmlString("<p>First paragraph</p><p>second paragraph</p>"),
                    Image = "test.png"
                };

                ViewResultMock.Object.ViewData.Model = viewModel;

                _sut = new PostTwitterMetaTagsAttribute();
            };

            Because of = () => _sut.OnActionExecuted(FilterContextMock.Object);

            It should_contain_correctly_formatted_metadata = () =>
            {
                var viewResult = FilterContextMock.Object.Result as ViewResultBase;
                viewResult.ShouldNotBeNull();

                var viewModel = viewResult.Model as PostViewModel;
                viewModel.MetaTitle.ShouldEqual(viewModel.PageTitle);
                viewModel.Site.ShouldBeTheSameAs(_expectedMetadata.Site);
                viewModel.Url.ShouldEqual(_expectedMetadata.Url);
                viewModel.Description.ShouldEqual(_expectedMetadata.Description);
                viewModel.Image.ShouldEqual(_expectedMetadata.Image);
                viewModel.HasImage.ShouldBeTrue();
            };
        }
    }
}
