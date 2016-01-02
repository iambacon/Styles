namespace IAmBacon.Web.Tests.Features
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using IAmBacon.Model.Entities;
    using IAmBacon.Presentation.Enumerations;
    using IAmBacon.ViewModels;
    using IAmBacon.Web.Tests.Context;

    using Machine.Specifications;
    using Machine.Specifications.Mvc;

    [Subject("Pagination")]
    public class Pagination
    {
        public class PaginationContext : PostContext
        {
            protected static Post Post;

            protected static ActionResult Result;

            Establish context = () =>
                {
                    Post = new Post
                    {
                        Tags = new List<Tag>(),
                        User = new User(),
                        Category = new Category()
                    };

                    CategoryServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Category>);
                };
        }

        public class When_there_are_is_one_page_or_less_of_posts : PaginationContext
        {
            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Empty<Post>);
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Tag>);
            };

            Because of = () => Result = PostController.Index();

            It should_not_display_pagination = () =>
                    Result.Model<PostsViewModel>().DisplayPagination.ShouldBeFalse();
        }

        public class When_there_are_more_than_one_page_of_posts : PaginationContext
        {
            Establish context = () =>
                {
                    PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Repeat(Post, 11));
                    TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Repeat(new Tag(), 3));
                };

            Because of = () => Result = PostController.Index();

            It should_display_pagination = () =>
            Result.Model<PostsViewModel>().DisplayPagination.ShouldBeTrue();

            It should_display_the_correct_number_of_links = () =>
            Result.Model<PostsViewModel>().Pagination.Pages.Count.ShouldEqual(2);

            It should_set_the_next_page =
                () => Result.Model<PostsViewModel>().Pagination.NextPage.PageNumber.ShouldEqual(2);
        }

        public class When_more_than_five_pages_of_posts : PaginationContext
        {
            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Repeat(Post, 51));
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Repeat(new Tag(), 3));
            };

            Because of = () => Result = PostController.Index();

            It should_display_links_to_the_first_five_pages = () =>
                {
                    var paginationLinks = Result.Model<PostsViewModel>().Pagination.Pages;
                    paginationLinks.Count().ShouldEqual(5);
                    paginationLinks.First().PageNumber.ShouldEqual(1);
                    paginationLinks.Last().PageNumber.ShouldEqual(5);
                };

            It should_display_the_first_page_as_the_current_page =
                () => Result.Model<PostsViewModel>().Pagination.Pages.First().IsCurrentPage.ShouldBeTrue();
        }

        public class When_more_than_five_pages_and_current_page_is_the_fifth : PaginationContext
        {
            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Repeat(Post, 51));
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Repeat(new Tag(), 3));
            };

            Because of = () => Result = PostController.Index(5);

            It should_display_a_link_to_the_first_page =
                () => Result.Model<PostsViewModel>().Pagination.Pages.First().PageNumber.ShouldEqual(1);

            private It should_display_an_ellipsis_for_the_second_link =
                () => Result.Model<PostsViewModel>().Pagination.Pages.ToList()[1].Type.ShouldEqual(PaginationItemType.Ellipsis);

            It should_display_the_fifth_page_as_the_current_page =
                () => Result.Model<PostsViewModel>().Pagination.Pages.First(x => x.PageNumber == 5).IsCurrentPage.ShouldBeTrue();

            It should_display_a_link_to_the_previous_page =
                () => Result.Model<PostsViewModel>().Pagination.Pages.FirstOrDefault(x => x.PageNumber == 4).ShouldNotBeNull();

            It should_display_a_link_to_the_next_page =
                () => Result.Model<PostsViewModel>().Pagination.Pages.FirstOrDefault(x => x.PageNumber == 6).ShouldNotBeNull();
        }

        public class When_more_than_five_pages_and_current_page_is_the_last : PaginationContext
        {
            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Repeat(Post, 53));
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Repeat(new Tag(), 3));
            };

            Because of = () => Result = PostController.Index(6);

            It should_display_the_last_link_as_the_current_page = () =>
            Result.Model<PostsViewModel>().Pagination.Pages.Last().IsCurrentPage.ShouldBeTrue();

            It should_set_next_page_as_null =
                () => Result.Model<PostsViewModel>().Pagination.NextPage.ShouldBeNull();
        }

        public class When_no_page_no_is_specified : PaginationContext
        {
            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Repeat(Post, 11));
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Repeat(new Tag(), 3));
            };

            Because of = () => Result = PostController.Index();

            It should_display_the_first_page_of_results = () =>
            Result.Model<PostsViewModel>().Posts.PageNumber.ShouldEqual(1);
        }

        public class When_a_page_no_is_specified : PaginationContext
        {
            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Repeat(Post, 11));
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Repeat(new Tag(), 3));
            };

            Because of = () => Result = PostController.Index(2);

            It should_display_the_specified_page_of_results = () =>
            Result.Model<PostsViewModel>().Posts.PageNumber.ShouldEqual(2);
        }

        public class When_a_page_of_items_is_returned : PaginationContext
        {
            Establish context = () =>
            {
                PostServiceMock.Setup(x => x.GetAllActive()).Returns(Enumerable.Repeat(Post, 11));
                TagServiceMock.Setup(x => x.GetAll()).Returns(Enumerable.Repeat(new Tag(), 3));
            };

            Because of = () => Result = PostController.Index();

            It should_contain_a_maximum_of_10_items = () =>
            Result.Model<PostsViewModel>().Posts.Count.ShouldEqual(10);
        }
    }
}
