using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace IAmBacon.Web.Tests.Controllers
{
    class PostControllerTests
    {
        [Subject("Categories")]
        public class When_I_browse_to_the_landing_page
        {
            It should_show_a_list_of_categories = null;
            It should_show_the_amount_of_posts_per_category = null;
        }

        [Subject("Categories")]
        public class When_I_click_on_a_category_link
        {
            It should_redirect_to_the_category_page = null;
        }

        [Subject("Categories")]
        public class When_there_are_no_categories
        {
            It should_display_no_categories = null;
        }
    }
}
