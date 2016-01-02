namespace IAmBacon.Web.Tests.Presentation.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using IAmBacon.Presentation.Extensions;

    using Machine.Specifications;

    using PagedList;

    public class PagedListExtensionsTests
    {
        private static IPagedList result;
        private static IEnumerable<int> items;

        [Subject("Display pagination")]
        public class When_first_page_of_many
        {
            Establish context = () => items = Enumerable.Repeat(1, 6);

            Because of = () => result = items.ToPagedList(1, 2);

            It should_return_true = () => result.DisplayPagination().ShouldBeTrue();
        }

        [Subject("Display pagination")]
        public class When_second_page_of_many
        {
            Establish context = () => items = Enumerable.Repeat(1, 6);

            Because of = () => result = items.ToPagedList(2, 2);

            It should_return_true = () => result.DisplayPagination().ShouldBeTrue();
        }

        [Subject("Display pagination")]
        public class When_last_page_of_many
        {
            Establish context = () => items = Enumerable.Repeat(1, 6);

            Because of = () => result = items.ToPagedList(3, 2);

            It should_return_true = () => result.DisplayPagination().ShouldBeTrue();
        }

        [Subject("Display pagination")]
        public class When_one_page
        {
            Establish context = () => items = Enumerable.Repeat(1, 2);

            Because of = () => result = items.ToPagedList(1, 2);

            It should_return_false = () => result.DisplayPagination().ShouldBeFalse();
        }

        [Subject("Display pagination")]
        public class When_no_items
        {
            Establish context = () => items = Enumerable.Empty<int>();

            Because of = () => result = items.ToPagedList(1, 2);

            It should_return_false = () => result.DisplayPagination().ShouldBeFalse();
        }
    }
}
