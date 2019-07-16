using System;
using System.Collections.Generic;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.Presentation.Models;
using IAmBacon.Admin.ViewModels.Post;
using IAmBacon.Core.Application.Infrastructure.Fakes;
using IAmBacon.Core.Application.Post.Commands;
using IAmBacon.Core.Application.Post.Queries;
using IAmBacon.Core.Application.PostCategory.Queries;
using IAmBacon.Core.Application.PostTag.Queries;
using IAmBacon.Core.Application.User.Queries;
using IAmBacon.Core.Infrastructure.Post;
using IAmBacon.Core.Infrastructure.Post.Repositories;
using IAmBacon.Core.Infrastructure.PostCategory;
using Machine.Specifications;
using Microsoft.EntityFrameworkCore;
using Category = IAmBacon.Core.Domain.AggregatesModel.PostAggregate.Category;
using Post = IAmBacon.Core.Domain.AggregatesModel.PostAggregate.Post;

namespace IAmBacon.Core.Admin.IntegrationTests.Controllers
{
    [Subject("Post controller - Create")]
    public class When_adding_a_post : Post_controller_command_context
    {
        Because of = async () =>
        {
            using (var categoryContext = new CategoryContext(CategoryOptions))
            using (var postContext = new PostContext(PostOptions))
            using (Sut = new PostController(new PostCommandHandler(new PostRepository(postContext)),
                UserQueries, CategoryQueries, TagQueries, PostQueries))
            {
                categoryContext.Categories.Add(new Category("Web"));

                var vm = new CreatePostViewModel
                {
                    Active = true,
                    AuthorId = 1,
                    CategoryId = 1,
                    Image = "image.png",
                    Markdown = "Text with *bold*",
                    Tags = new List<CheckboxItem>
                    {
                        new CheckboxItem {Id = 1, IsChecked = true, Label = "css"},
                        new CheckboxItem {Id = 2, IsChecked = false, Label = "sass"}
                    },
                    Title = "Blog post"
                };

                await Sut.Create(vm);
            }
        };

        It should_write_to_the_db = () =>
        {
            using (var context = new PostContext(PostOptions))
            {
                context.Posts.Count().ShouldEqual(1);
                context.PostTags.Count().ShouldEqual(1);
            }
        };
    }

    [Subject("Post controller - Update")]
    public class When_updating_a_post : Post_controller_command_context
    {
        Establish context = () =>
        {
            using (var context = new PostContext(PostOptions))
            {
                var post = new Post(1, 1, "post title", "this is a blog post");
                post.SetImage("image.png");
                post.SetTags(new[] { 1 });

                context.Posts.Add(post);
            }
        };

        private Because of = async () =>
        {
            using (var context = new PostContext(PostOptions))
            using (Sut = new PostController(new PostCommandHandler(new PostRepository(context)), UserQueries, CategoryQueries, TagQueries, PostQueries))
            {
                var vm = new EditPostViewModel
                {
                    Active = true,
                    AuthorId = 2,
                    CategoryId = 2,
                    Image = "new.png",
                    Markdown = "edit post",
                    NoCss = true,
                    PostId = 1,
                    Title = "New title",
                    Tags = new List<CheckboxItem> { new CheckboxItem { Id = 2, IsChecked = true, Label = "css" } }
                };

                await Sut.Edit(vm);
            }
        };

        It should_update_deleted_state = async () =>
        {
            using (var context = new PostContext(PostOptions))
            {
                var post = await context.Posts.FindAsync(1);
                post.ShouldNotBeNull();
                post.Deleted.ShouldBeFalse();
            }
        };

        It should_update_active_state = async () =>
        {
            using (var context = new PostContext(PostOptions))
            {
                var post = await context.Posts.FindAsync(1);
                post.ShouldNotBeNull();
                post.IsActive.ShouldBeTrue();
            }
        };

        It should_update_the_image;

        It should_update_noCss_state;

        It should_update_tags;

        It should_update_the_author;

        It should_update_the_category;

        It should_update_the_title;

        It should_update_the_content;
    }

    public abstract class Post_controller_command_context
    {
        Establish context = () =>
        {
            var dbName = Guid.NewGuid().ToString();

            PostOptions = new DbContextOptionsBuilder<PostContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            CategoryOptions = new DbContextOptionsBuilder<CategoryContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            CategoryQueries = new CategoryQueries(new DapperDbConnectionFactoryFake());
            UserQueries = new UserQueries(new DapperDbConnectionFactoryFake());
            TagQueries = new TagQueries(new DapperDbConnectionFactoryFake());
            PostQueries = new PostQueries(new DapperDbConnectionFactoryFake());
        };

        protected static PostController Sut;
        protected static DbContextOptions<PostContext> PostOptions;
        protected static DbContextOptions<CategoryContext> CategoryOptions;
        protected static ICategoryQueries CategoryQueries;
        protected static IUserQueries UserQueries;
        protected static ITagQueries TagQueries;
        protected static IPostQueries PostQueries;
    }
}
