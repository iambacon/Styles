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
    public class PostContext
    {
        protected static PostController postController;

        protected static Mock<IPostService> postServiceMock;

        private static Mock<ICommentService> commentServiceMock;

        protected static Mock<ITagService> tagServiceMock;

        protected static Mock<ICategoryService> categoryServiceMock;

        private static Mock<ISpamManager> spamManagerMock;

        private static Mock<IEmailManager> emailManagerMock;

        Establish context = () =>
        {
            postServiceMock = new Mock<IPostService>(MockBehavior.Strict);
            commentServiceMock = new Mock<ICommentService>(MockBehavior.Strict);
            tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            categoryServiceMock = new Mock<ICategoryService>(MockBehavior.Strict);
            spamManagerMock = new Mock<ISpamManager>(MockBehavior.Strict);
            emailManagerMock = new Mock<IEmailManager>(MockBehavior.Strict);

            postServiceMock.Setup(x => x.GetLatest(MoqIt.IsAny<int>())).Returns(new List<Post>());

            postController =
                new PostController(postServiceMock.Object,
                    commentServiceMock.Object,
                    tagServiceMock.Object,
                    categoryServiceMock.Object,
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
