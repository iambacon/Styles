using System;
using System.Collections.Generic;
using System.Linq;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.Presentation.Models;
using IAmBacon.Admin.ViewModels.Post;
using IAmBacon.Core.Application.Infrastructure.Fakes;
using IAmBacon.Core.Application.Post.Commands;
using IAmBacon.Core.Application.PostCategory.Queries;
using IAmBacon.Core.Application.PostTag.Queries;
using IAmBacon.Core.Application.User.Queries;
using IAmBacon.Core.Infrastructure.Post;
using IAmBacon.Core.Infrastructure.Post.Repositories;
using IAmBacon.Core.Infrastructure.PostCategory;
using Machine.Specifications;
using Microsoft.EntityFrameworkCore;
using Category = IAmBacon.Core.Domain.AggregatesModel.PostAggregate.Category;

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
                UserQueries, CategoryQueries, TagQueries))
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
        };

        protected static PostController Sut;
        protected static DbContextOptions<PostContext> PostOptions;
        protected static DbContextOptions<CategoryContext> CategoryOptions;
        protected static ICategoryQueries CategoryQueries;
        protected static IUserQueries UserQueries;
        protected static ITagQueries TagQueries;
    }
}
