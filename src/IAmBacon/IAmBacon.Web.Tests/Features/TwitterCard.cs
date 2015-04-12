using System;
using System.Web;
using System.Web.Mvc;
using IAmBacon.Attributes;
using IAmBacon.ViewModels;
using IAmBacon.ViewModels.Home;
using IAmBacon.Web.Tests.Context;
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
                Assert.AreSame(viewModel.Title, viewModel.PageTitle);
                Assert.AreSame(viewModel.Site, _expectedMetadata.Site);
                Assert.IsTrue(viewModel.Url.Equals(_expectedMetadata.Url, StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.Description.Equals(_expectedMetadata.Description,
                    StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.Image.Equals(_expectedMetadata.Image,
                    StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(viewModel.HasImage);
            };

            It should_return_the_canonical_url;

            It should_allow_the_image_to_be_optional;
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
                Assert.IsNotNull(viewResult);

                var viewModel = viewResult.Model as PostViewModel;
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
