
namespace IAmBacon.Presentation.HtmlHelpers
{
    using System;
    using System.Configuration;
    using System.Web;

    /// <summary>
    /// Static class for google html helpers.
    /// </summary>
    public static class Google
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the google analytics HTML.
        /// </summary>
        /// <param name="id">
        /// The account id.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString GetAnalyticsHtml(string id = null)
        {
            string accountId = id;

            if (string.IsNullOrWhiteSpace(accountId))
            {
                accountId = ConfigurationManager.AppSettings["GoogleAnalyticsAccount"];
            }

            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ApplicationException(
                    "You must specify a Google Account Id, you can for example use appSettings with the key 'GoogleAnalyticAccount' in web.config or pass the id as an argument");
            }

            return
                new HtmlString(
                    @"
                            <script type=""text/javascript"">
                            var _gaq = _gaq || [];
                            _gaq.push(['_setAccount', '"
                    + accountId
                    +
                    @"']);
                            _gaq.push(['_trackPageview']);

                            (function () {
                                var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                                ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
                            })();
                            </script>");
        }

        #endregion
    }
}