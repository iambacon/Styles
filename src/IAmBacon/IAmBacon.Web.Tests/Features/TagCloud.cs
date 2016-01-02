using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IAmBacon.Model.Entities;
using IAmBacon.ViewModels;
using IAmBacon.ViewModels.Post;
using IAmBacon.Web.Tests.Context;
using Machine.Specifications;
using Machine.Specifications.Mvc;

namespace IAmBacon.Web.Tests.Features
{
    [Subject("Tag Cloud")]
    public class TagCloud
    {
        public class When_I_browse_to_the_landing_page : PostContext
        {
            private static List<Post> _posts;

            private static List<Tag> _tags;

            static ActionResult _result;

            Establish context = () =>
            {
                var categories = new List<Category>
                {
                    new Category{Name = "Category 1"},
                    new Category{Name = "Category 1"},
                    new Category{Name = "Category 1"},
                    new Category{Name = "Category 2"}
                };

                _posts = new List<Post>
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

                _tags = new List<Tag>
                {
                    new Tag{Name = "bTag1", SeoName = "tag-1"},
                    new Tag{Name = "cTag2", SeoName = "tag-2"},
                    new Tag{Name = "aTag3", SeoName = "tag-3"}
                };

                PostServiceMock.Setup(x => x.GetAll()).Returns(_posts);
                CategoryServiceMock.Setup(x => x.GetAll()).Returns(categories);
                TagServiceMock.Setup(x => x.GetAll()).Returns(_tags);
            };

            Because of = () => _result = PostController.Index();

            It should_show_a_list_of_all_the_tags = () =>
                _result.Model<PostsViewModel>().Tags.ShouldBeOfExactType<List<TagViewModel>>();

            It should_order_tags_alphabetically = () =>
            {
                var expectedFirstTag = _tags.OrderBy(x => x.Name).First();
                var expectedLastTag = _tags.OrderBy(x => x.Name).Last();

                _result.Model<PostsViewModel>().Tags.First().Name.ShouldEqual(expectedFirstTag.Name);
                _result.Model<PostsViewModel>().Tags.Last().Name.ShouldEqual(expectedLastTag.Name);
            };
        }

        public class When_there_are_no_tags : PostContext
        {
            static ActionResult _result;

            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Post>);
                CategoryServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Category>);
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Tag>);
            };

            Because of = () => _result = PostController.Index();

            It should_display_no_tags = () => _result.Model<PostsViewModel>().DisplayTags.ShouldEqual(false);
        }
    }
}
