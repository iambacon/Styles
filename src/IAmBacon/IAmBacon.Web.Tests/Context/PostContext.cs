using System.Collections.Generic;
using IAmBacon.Controllers;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Domain.Smtp.Interfaces;
using IAmBacon.Domain.Utilities.Interfaces;
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

        private static Mock<ICommentService> commentServiceMock;

        protected static Mock<ITagService> TagServiceMock;

        protected static Mock<ICategoryService> CategoryServiceMock;

        private static Mock<ISpamManager> spamManagerMock;

        private static Mock<IEmailManager> emailManagerMock;

        private Establish context = () =>
        {
            Fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            PostServiceMock = new Mock<IPostService>();
            commentServiceMock = new Mock<ICommentService>(MockBehavior.Strict);
            TagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            CategoryServiceMock = new Mock<ICategoryService>(MockBehavior.Strict);
            spamManagerMock = new Mock<ISpamManager>(MockBehavior.Strict);
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
                    spamManagerMock.Object,
                    emailManagerMock.Object)
                {
                    Url = url
                };
        };
    }
}
