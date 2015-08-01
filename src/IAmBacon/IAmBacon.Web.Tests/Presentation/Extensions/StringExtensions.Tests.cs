namespace IAmBacon.Web.Tests.Presentation.Extensions
{
    using System.Web;

    using IAmBacon.Presentation.Extensions;

    using Machine.Specifications;

    [Subject("String extensions")]
    public class StringExtensions
    {
        [Subject("Get summary")]
        public class When_first_paragraph_of_html_contains_img_tag
        {
            static string html = "<p><img src='http://images.iambacon.co.uk/blog/powershell.png' alt='Powershell console'></p>"
                                    + "<p>This one has been annoying me for a while.</p>"
                                    + "<p>When Powershell opens, it's tiny.</p>";

            static string expectedHtml = "<p><img src='http://images.iambacon.co.uk/blog/powershell.png' alt='Powershell console'></p>"
                                            + "<p>This one has been annoying me for a while.</p>";

            static IHtmlString result;

            Because of = () => result = html.GetSummary();

            It should_return_the_first_two_paragraphs_of_html = () =>
                result.ToString().ShouldEqual(expectedHtml);
        }

        [Subject("Get summary")]
        public class When_html_does_not_contain_img_tag
        {
            static string html = "<p>This one has been annoying me for a while.</p>"
                                    + "<p>When Powershell opens, it's tiny.</p>";
            static string expectedHtml = "<p>This one has been annoying me for a while.</p>";
            static IHtmlString result;

            Because of = () => result = html.GetSummary();

            It should_return_type_HtmlString = () => result.ShouldBeOfExactType<HtmlString>();

            It should_return_the_first_paragraph_of_html = () =>
                result.ToString().ShouldEqual(expectedHtml);
        }

        [Subject("Get first paragraph")]
        public class When_html_contains_p_tags
        {
            static IHtmlString html =
                new HtmlString(
                    "<p>Powershell console</p>"
                    + "<p>This one has been annoying me for a while.</p>");

            static string expectedHtml = "Powershell console";
            static string result;

            Because of = () => result = html.GetFirstParagraph();

            It should_return_the_contents_of_the_first_occuring_p_tag = () =>
                result.ShouldEqual(expectedHtml);
        }
    }
}
