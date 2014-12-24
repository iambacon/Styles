using IAmBacon.Web.Tests.Context;
using Machine.Specifications;

namespace IAmBacon.Web.Tests.Features
{
    [Subject("Blog post")]
    public class BlogPost
    {
        public class When_I_browse_to_a_post : PostContext
        {
            It should_show_the_tags_for_the_post;
        }

        public class When_there_are_no_post_tags
        {
            It should_not_show_any_tags;
        }
    }
}
