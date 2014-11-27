using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IAmBacon.Controllers;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Model.Entities;
using IAmBacon.ViewModels.Home;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IAmBacon.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController controller;

        private Mock<IPostService> postService;

        [TestInitialize]
        public void Setup()
        {
            SetupServices();

            this.controller = new HomeController(this.postService.Object);
        }

        [TestMethod]
        public void Should_See_The_Blog_Section_With_Six_Items()
        {
            // Arrange
            const int expectedResult = 6;
            var post = new Post
            {
                Category = new Category(),
                User = new User(),
                Image = "http://some.url"
            };
            var latestPosts = Enumerable.Repeat(post, expectedResult);

            this.postService
                .Setup(x => x.GetLatest(It.IsAny<int>()))
                .Returns(latestPosts);

            // Act
            var result = this.controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(HomeViewModel));
            var model = (HomeViewModel)viewResult.Model;

            Assert.IsNotNull(model.BlogPosts);
            Assert.AreEqual(expectedResult, model.BlogPosts.Count());
        }

        public void Should_See_The_Blog_Section_Ordered_By_Latest()
        {
            // TODO: Write this test.
        }

        [TestMethod]
        public void Should_Not_See_The_Blog_Section_When_There_Are_No_Items()
        {
            // Arrange

            // Act
            var result = this.controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(HomeViewModel));
            var model = (HomeViewModel)viewResult.Model;

            Assert.IsFalse(model.ShowBlogPosts);
        }

        private void SetupServices()
        {
            this.postService = new Mock<IPostService>(MockBehavior.Strict);

            this.postService
                .Setup(x => x.GetLatest(It.IsAny<int>()))
                .Returns((List<Post>)null);

            this.postService
                .Setup(x => x.GetPopular(It.IsAny<int>()))
                .Returns((List<Post>)null);
        }
    }
}
