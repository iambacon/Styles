using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IAmBacon.Controllers;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Domain.Smtp.Interfaces;
using IAmBacon.Domain.Utilities.Interfaces;
using IAmBacon.Framework.Mvc;
using IAmBacon.Model.Entities;
using IAmBacon.ViewModels;
using IAmBacon.ViewModels.Post;
using IAmBacon.Web.Tests.Helpers;
using Machine.Specifications;
using Machine.Specifications.Mvc;
using Moq;
using It = Machine.Specifications.It;
using MoqIt = Moq.It;

namespace IAmBacon.Web.Tests.Controllers
{
    public class PostControllerTests
    {
        public class Post_controller_context
        {
            protected static PostController postController;

            protected static Mock<IPostService> postServiceMock;

            private static Mock<ICommentService> commentServiceMock;

            private static Mock<ITagService> tagServiceMock;

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

            Because of = () => result = postController.Index();

            protected static ActionResult result;
        }

        [Subject("Categories")]
        public class When_I_browse_to_the_landing_page : Post_controller_context
        {
            Establish context = () =>
            {
                var categories = new List<Category>
                {
                    new Category{Name = "Category 1"},
                    new Category{Name = "Category 1"},
                    new Category{Name = "Category 1"},
                    new Category{Name = "Category 2"}
                };

                posts = new List<Post>
                {
                    new Post
                    {
                        Category = new Category{Name = "Category 1"}, 
                        DateCreated = DateTime.Today, 
                        Content = string.Empty, 
                        Tags = new List<Tag>(), 
                        User = new User(),
                        Title = "Post 1"
                    },
                    new Post
                    {
                        Category = new Category{Name = "Category 1"}, 
                        DateCreated = DateTime.Today.AddDays(-5), 
                        Content = string.Empty, 
                        Tags = new List<Tag>(), 
                        User = new User(),
                        Title = "Post 2"
                    },
                    new Post
                    {
                        Category = new Category{Name = "Category 2"}, 
                        DateCreated = DateTime.Today.AddDays(-2), 
                        Content = string.Empty, 
                        Tags = new List<Tag>(), 
                        User = new User(),
                        Title = "Post 3"
                    },
                    new Post
                    {
                        Category = new Category{Name = "Category 1"}, 
                        DateCreated = DateTime.Today.AddDays(-3), 
                        Content = string.Empty, 
                        Tags = new List<Tag>(), 
                        User = new User(),
                        Title = "Post 4"
                    }
                };

                categoryServiceMock.Setup(x => x.GetAll()).Returns(categories);
                postServiceMock.Setup(x => x.GetAll()).Returns(posts);
            };

            It should_show_a_list_of_categories = () =>
                result.Model<PostsViewModel>().CategorySummaries.ShouldBeOfExactType<List<CategoryCountViewModel>>();

            It should_show_the_amount_of_posts_per_category = () =>
                result.Model<PostsViewModel>().CategorySummaries.First().Count.ShouldEqual(ExpectedCategoryCount);

            It should_show_the_amount_of_posts_per_category_as_a_percentage = () =>
                result.Model<PostsViewModel>().CategorySummaries.First().Percent.ShouldEqual(ExpectedPercent);

            It should_order_posts_by_date_created_desc = () =>
                result.Model<PostsViewModel>().Posts.ShouldBeSortedByDateInDescendingOrder();

            private static List<Post> posts;
            private const int ExpectedCategoryCount = 3;
            private const double ExpectedPercent = 0.75;
        }

        [Subject("Categories")]
        public class When_there_are_no_categories : Post_controller_context
        {
            Establish context = () => categoryServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Category>());

            It should_display_no_categories = () =>
                result.Model<PostsViewModel>().DisplayCategories.ShouldEqual(ExpectedResult);

            private const bool ExpectedResult = false;
        }
    }
}
