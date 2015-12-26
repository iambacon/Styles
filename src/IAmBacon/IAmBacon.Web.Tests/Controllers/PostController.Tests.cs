﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IAmBacon.Model.Entities;
using IAmBacon.ViewModels;
using IAmBacon.ViewModels.Post;
using IAmBacon.Web.Tests.Context;
using IAmBacon.Web.Tests.Helpers;
using Machine.Specifications;
using Machine.Specifications.Mvc;
using It = Machine.Specifications.It;
using MoqIt = Moq.It;

namespace IAmBacon.Web.Tests.Controllers
{
    public class PostControllerTests
    {
        public class Post_controller_context : PostContext
        {
            protected static ActionResult result;

            Because of = () => result = postController.Index();
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

                var tags = new List<Tag>
                {
                    new Tag{Name = "Tag1", SeoName = "tag-1"},
                    new Tag{Name = "Tag2", SeoName = "tag-2"},
                    new Tag{Name = "Tag3", SeoName = "tag-3"}
                };

                categoryServiceMock.Setup(x => x.GetAll()).Returns(categories);
                postServiceMock.Setup(x => x.GetAllActive()).Returns(posts);
                tagServiceMock.Setup(x => x.GetAll()).Returns(tags);
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
            Establish context = () =>
            {
                postServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Empty<Post>);
                categoryServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Category>());
                tagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Tag>);
            };

            It should_display_no_categories = () =>
                result.Model<PostsViewModel>().DisplayCategories.ShouldEqual(ExpectedResult);

            private const bool ExpectedResult = false;
        }

        [Subject("Blog posts")]
        public class When_there_are_posts : Post_controller_context
        {
            private Establish context = () =>
                {
                    postServiceMock.Setup(x => x.GetAllActive()).Returns(new List<Post>());
                    categoryServiceMock.Setup(x => x.GetAll()).Returns(new List<Category>());
                    tagServiceMock.Setup(x => x.GetAll()).Returns(new List<Tag>());
                };

            private It should_only_display_active_posts = () =>
                {
                    postServiceMock.Verify(x => x.GetAllActive());
                };
        }
    }
}
