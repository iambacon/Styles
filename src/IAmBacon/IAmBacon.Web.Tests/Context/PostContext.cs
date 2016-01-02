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
    /// <summary>
    /// Context class for the <see cref="PostController"/>.
    /// </summary>
    public class PostContext
    {
        protected static PostController PostController;

        protected static Mock<IPostService> PostServiceMock;

        private static Mock<ICommentService> commentServiceMock;

        protected static Mock<ITagService> TagServiceMock;

        protected static Mock<ICategoryService> CategoryServiceMock;

        private static Mock<ISpamManager> spamManagerMock;

        private static Mock<IEmailManager> emailManagerMock;

        Establish context = () =>
        {
            PostServiceMock = new Mock<IPostService>();
            commentServiceMock = new Mock<ICommentService>(MockBehavior.Strict);
            TagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            CategoryServiceMock = new Mock<ICategoryService>(MockBehavior.Strict);
            spamManagerMock = new Mock<ISpamManager>(MockBehavior.Strict);
            emailManagerMock = new Mock<IEmailManager>(MockBehavior.Strict);

            PostServiceMock.Setup(x => x.GetLatest(MoqIt.IsAny<int>())).Returns(new List<Post>());

            PostController =
                new PostController(PostServiceMock.Object,
                    commentServiceMock.Object,
                    TagServiceMock.Object,
                    CategoryServiceMock.Object,
                    spamManagerMock.Object,
                    emailManagerMock.Object)
                {
                    Url =
                        Mock.Of<IUrlHelper>(
                            x => x.Action(MoqIt.IsAny<string>(), MoqIt.IsAny<object>()) == "http://www.test.com")
                };
        };
    }
}
