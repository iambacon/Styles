using IAmBacon.Core.Domain.Utilities;
using Machine.Specifications;

namespace IAmBacon.Core.Domain.Tests.Utilities
{
    [Subject("Seo title")]
    public class When_title_is_mixed_case
    {
        Establish context = () => _input = "UpPer";

        Because of = () => _result = Seo.Title(_input);

        It should_lowercase_all_characters = () => _result.ShouldBeLike(_input.ToLower());

        static string _result;
        static string _input;
    }

    [Subject("Seo Title")]
    public class When_title_contains_invalid_characters
    {
        Establish context = () => _input = "1.2";

        Because of = () => _result = Seo.Title(_input);

        It should_replace__the_invalid_characters_with_dashes = () => _result.ShouldBeLike("1-2");

        static string _result;
        static string _input;
    }

    [Subject("Seo Title")]
    public class When_title_contains_c_sharp
    {
        Establish context = () => _input = "c#";

        Because of = () => _result = Seo.Title(_input);

        It should_use_the_alphabetical_representation = () => _result.ShouldBeLike("c-sharp");

        static string _result;
        static string _input;
    }

    [Subject("Seo Title")]
    public class When_title_contains_an_ampersand
    {
        Establish context = () => _input = "this & that";

        Because of = () => _result = Seo.Title(_input);

        It should_use_the_word_and = () => _result.ShouldBeLike("this-and-that");

        static string _result;
        static string _input;
    }

    [Subject("Seo Title")]
    public class When_title_contains_duplicate_dashes
    {
        Establish context = () => _input = "no!! way";

        Because of = () => _result = Seo.Title(_input);

        It should_remove_the_duplicates = () => _result.ShouldBeLike("no-way");

        static string _result;
        static string _input;
    }

    [Subject("Seo Title")]
    public class When_title_contains_whitespace
    {
        Establish context = () => _input = " hello ";

        Because of = () => _result = Seo.Title(_input);

        It should_remove_trim_leading_and_trailing_characters = () => _result.ShouldBeLike("hello");

        static string _result;
        static string _input;
    }

    [Subject("Seo Title")]
    public class When_title_contains_apostrophe
    {
        Establish context = () => _input = "it's";

        Because of = () => _result = Seo.Title(_input);

        It should_remove_trim_leading_and_trailing_characters = () => _result.ShouldBeLike("its");

        static string _result;
        static string _input;
    }

    [Subject("Seo title")]
    public class When_argument_null
    {
        Because of = () => _result = Seo.Title(null);

        It should_return_an_empty_string = () => _result.ShouldBeEmpty();

        static string _result;
    }
}
