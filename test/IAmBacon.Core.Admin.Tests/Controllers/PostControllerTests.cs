using System;
using System.Collections.Generic;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.Presentation.Models;
using IAmBacon.Admin.ViewModels.Post;
using IAmBacon.Core.Application.Post.Commands;
using IAmBacon.Core.Application.Post.Queries.Fakes;
using IAmBacon.Core.Application.PostCategory.Queries.Fakes;
using IAmBacon.Core.Application.PostTag.Queries.Fakes;
using IAmBacon.Core.Application.User.Queries;
using IAmBacon.Core.Application.User.Queries.Fakes;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;
using IAmBacon.Core.Infrastructure.Post.Fakes;
using IAmBacon.Core.Infrastructure.Post.Repositories.Fakes;
using IAmBacon.Core.Infrastructure.PostTag.Fakes;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("Post controller")]
    public class PostControllerTests
    {
        public class When_userQueries_argument_null
        {
            Establish context = () =>
            {
                var postContext = new PostContextFake();
                var tagContext = new TagContextFake();
                var postRepo = new PostRepositoryFake(postContext);
                _handler = new PostCommandHandler(postRepo);
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new PostController(_handler, null, null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static PostController _sut;
            static Exception _exception;
            static PostCommandHandler _handler;
        }

        public class When_categoryQueries_argument_null
        {
            Establish context = () =>
            {
                var postContext = new PostContextFake();
                var tagContext = new TagContextFake();
                var postRepo = new PostRepositoryFake(postContext);
                _handler = new PostCommandHandler(postRepo);
                _userQueries = new UserQueriesFake();
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new PostController(_handler, _userQueries, null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static PostController _sut;
            static Exception _exception;
            static UserQueriesFake _userQueries;
            static PostCommandHandler _handler;
        }

        public class When_tagQueries_argument_null
        {
            Establish context = () =>
            {
                var postContext = new PostContextFake();
                var tagContext = new TagContextFake();
                var postRepo = new PostRepositoryFake(postContext);
                _handler = new PostCommandHandler(postRepo);
                _userQueries = new UserQueriesFake();
                _categoryQueries = new CategoryQueriesFake();
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new PostController(_handler, _userQueries, _categoryQueries, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static PostController _sut;
            static Exception _exception;
            static UserQueriesFake _userQueries;
            static CategoryQueriesFake _categoryQueries;
            static PostCommandHandler _handler;
        }

        public class When_postQueries_argument_null
        {
            Establish context = () =>
            {
                var postContext = new PostContextFake();
                var tagContext = new TagContextFake();
                var postRepo = new PostRepositoryFake(postContext);
                _handler = new PostCommandHandler(postRepo);
                _userQueries = new UserQueriesFake();
                _categoryQueries = new CategoryQueriesFake();
                _tagQueries = new TagQueriesFake();
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new PostController(_handler, _userQueries, _categoryQueries, _tagQueries, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static PostController _sut;
            static Exception _exception;
            static UserQueriesFake _userQueries;
            static CategoryQueriesFake _categoryQueries;
            static TagQueriesFake _tagQueries;
            static PostCommandHandler _handler;
        }
    }

    [Subject("Post controller Create")]
    public class PostControllerCreate
    {
        public class When_get : Post_controller_context
        {
            Establish context = () =>
            {
                var category = new Application.PostCategory.Queries.Category
                {
                    Id = 1,
                    Name = "css"
                };

                CategoryQueries.Add(category);

                var tag = new Application.PostTag.Queries.Tag
                {
                    Id = 1,
                    Name = "sass"
                };

                TagQueries.Add(tag);

                var user = new User
                {
                    Id = 1,
                    FirstName = "Rob",
                    LastName = "Bob"
                };

                UserQueries.Add(user);
            };

            Because of = async () => Result = await Sut.Create();

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();

            It should_return_a_list_of_users = () =>
            {
                var vm = ((ViewResult)Result).Model as CreatePostViewModel;
                vm.Authors.Count.ShouldEqual(1);
            };

            It should_return_a_list_of_categories = () =>
            {
                var vm = ((ViewResult)Result).Model as CreatePostViewModel;
                vm.Categories.Count.ShouldEqual(1);
            };

            It should_return_a_list_of_tags = () =>
            {
                var vm = ((ViewResult)Result).Model as CreatePostViewModel;
                vm.Tags.Count.ShouldEqual(1);
            };
        }

        public class When_post_and_modelstate_is_invalid : Post_controller_context
        {
            Establish context = () => Sut.ModelState.AddModelError("Title", "Required");

            Because of = async () => Result = await Sut.Create(new CreatePostViewModel());

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();

            It should_return_modelState_error = () => ((ViewResult)Result).ViewData.ModelState.ErrorCount.ShouldEqual(1);
        }

        public class When_post_and_modelState_is_valid : Post_controller_context
        {
            Because of = async () => Result = await Sut.Create(null);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }
    }

    [Subject("Post controller Edit")]
    public class PostControllerEdit
    {
        public class When_get : Post_controller_context
        {
            Establish context = () =>
            {
                var entity = new Application.Post.Queries.Post
                {
                    Id = 1,
                    Title = "New post"
                };

                PostQueries.Add(entity);
            };

            Because of = async () => Result = await Sut.Edit(1);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_get_and_post_does_not_exist : Post_controller_context
        {
            Because of = async () => Result = await Sut.Edit(1);

            It should_return_not_found = () => Result.ShouldBeOfExactType<NotFoundResult>();
        }

        public class When_post_and_modelState_is_invalid : Post_controller_context
        {
            Establish context = () => Sut.ModelState.AddModelError("Title", "Required");

            Because of = async () => Result = await Sut.Edit(new EditPostViewModel());

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();

            It should_return_modelState_error = () => ((ViewResult)Result).ViewData.ModelState.ErrorCount.ShouldEqual(1);
        }

        public class When_post_and_modelState_is_valid : Post_controller_context
        {
            Establish context = () => Repo.Add(new Post(1, 1, "Title", "This is a post"));

            Because of = async () =>
            {
                var vm = new EditPostViewModel
                { AuthorId = 1, CategoryId = 1, Tags = new List<CheckboxItem>(), Title = "Title", Markdown = "This is a post" };

                Result = await Sut.Edit(vm);
            };

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<RedirectToActionResult>();
        }
    }

    [Subject("Post controller Delete")]
    public class PostControllerDelete
    {
        public class When_get : Post_controller_context
        {
            Establish context = () =>
            {
                var entity = new Application.Post.Queries.Post
                {
                    Id = 1,
                    Title = "New post"
                };

                PostQueries.Add(entity);
            };

            Because of = async () => Result = await Sut.Delete(1);

            It should_return_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();
        }

        public class When_get_and_post_does_not_exist : Post_controller_context
        {
            Because of = async () => Result = await Sut.Delete(1);

            It should_return_not_found = () => Result.ShouldBeOfExactType<NotFoundResult>();
        }
    }

    public abstract class Post_controller_context
    {
        Establish context = () =>
        {
            var postContext = new PostContextFake();
            Repo = new PostRepositoryFake(postContext);
            var handler = new PostCommandHandler(Repo);

            UserQueries = new UserQueriesFake();
            CategoryQueries = new CategoryQueriesFake();
            TagQueries = new TagQueriesFake();
            PostQueries = new PostQueriesFake();
            Sut = new PostController(handler, UserQueries, CategoryQueries, TagQueries, PostQueries);
        };

        protected static PostController Sut;
        protected static IActionResult Result;
        protected static UserQueriesFake UserQueries;
        protected static CategoryQueriesFake CategoryQueries;
        protected static TagQueriesFake TagQueries;
        protected static PostQueriesFake PostQueries;
        protected static PostRepositoryFake Repo;
    }
}
