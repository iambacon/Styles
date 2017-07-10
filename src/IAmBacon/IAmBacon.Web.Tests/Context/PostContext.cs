using System.Collections.Generic;
using IAmBacon.Controllers;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Domain.Smtp.Interfaces;
using IAmBacon.Framework.Mvc;
using IAmBacon.Model.Entities;
using Machine.Specifications;
using Moq;
using MoqIt = Moq.It;

namespace IAmBacon.Web.Tests.Context
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    /// <summary>
    /// Context class for the <see cref="PostController"/>.
    /// </summary>
    public class PostContext
    {
        protected static IFixture Fixture;

        protected static PostController PostController;

        protected static Mock<IPostService> PostServiceMock;

        private static Mock<IService<Comment>> commentServiceMock;

        protected static Mock<IService<Tag>> TagServiceMock;

        protected static Mock<IService<Category>> CategoryServiceMock;
        
        private static Mock<IEmailManager> emailManagerMock;

        private Establish context = () =>
        {
            Fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            PostServiceMock = new Mock<IPostService>();
            commentServiceMock = new Mock<IService<Comment>>(MockBehavior.Strict);
            TagServiceMock = new Mock<IService<Tag>>(MockBehavior.Strict);
            CategoryServiceMock = new Mock<IService<Category>>(MockBehavior.Strict);
            emailManagerMock = new Mock<IEmailManager>(MockBehavior.Strict);

            PostServiceMock.Setup(x => x.GetLatest(MoqIt.IsAny<int>())).Returns(new List<Post>());

            IUrlHelper url = Mock.Of<IUrlHelper>();

            Mock.Get(url)
                .Setup(x => x.Action(MoqIt.IsAny<string>(), MoqIt.IsAny<object>())).Returns("http://www.iambacon.co.uk/Action");

            Mock.Get(url)
                .Setup(x => x.RouteUrl(MoqIt.IsAny<string>(), MoqIt.IsAny<object>(), MoqIt.IsAny<string>()))
                .Returns("http://www.iambacon.co.uk/Route");

            PostController =
                new PostController(PostServiceMock.Object,
                    commentServiceMock.Object,
                    TagServiceMock.Object,
                    CategoryServiceMock.Object,
                    emailManagerMock.Object)
                {
                    Url = url
                };
        };
    }
}
