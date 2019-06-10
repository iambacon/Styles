using System;
using IAmBacon.Admin.Controllers;
using IAmBacon.Admin.ViewModels.Post;
using IAmBacon.Core.Application.PostCategory.Queries.Fakes;
using IAmBacon.Core.Application.PostTag.Queries.Fakes;
using IAmBacon.Core.Application.User.Queries;
using IAmBacon.Core.Application.User.Queries.Fakes;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace IAmBacon.Core.Admin.Tests.Controllers
{
    [Subject("Post controller")]
    public class PostControllerTests
    {
        public class When_userQueries_argument_null
        {
            Because of = () => _exception = Catch.Exception(() => _sut = new PostController(null, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static PostController _sut;
            static Exception _exception;
        }

        public class When_categoryQueries_argument_null
        {
            Establish context = () =>
            {
                _userQueries = new UserQueriesFake();
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new PostController(_userQueries, null, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static PostController _sut;
            static Exception _exception;
            private static UserQueriesFake _userQueries;
        }

        public class When_tagQueries_argument_null
        {
            Establish context = () =>
            {
                _userQueries = new UserQueriesFake();
                _categoryQueries = new CategoryQueriesFake();
            };

            Because of = () => _exception = Catch.Exception(() => _sut = new PostController(_userQueries, _categoryQueries, null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_be_of_type_ArgumentNullException = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static PostController _sut;
            static Exception _exception;
            private static UserQueriesFake _userQueries;
            private static CategoryQueriesFake _categoryQueries;
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
    }

    public abstract class Post_controller_context
    {
        Establish context = () =>
        {
            UserQueries = new UserQueriesFake();
            CategoryQueries = new CategoryQueriesFake();
            TagQueries = new TagQueriesFake();
            Sut = new PostController(UserQueries, CategoryQueries, TagQueries);
        };

        protected static PostController Sut;
        protected static IActionResult Result;
        protected static UserQueriesFake UserQueries;
        protected static CategoryQueriesFake CategoryQueries;
        protected static TagQueriesFake TagQueries;
    }
}
