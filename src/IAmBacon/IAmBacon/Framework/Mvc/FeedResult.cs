namespace IAmBacon.Framework.Mvc
{
    using System;
    using System.ServiceModel.Syndication;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml;

    /// <summary>
    /// Feed action result for rss feeds.
    /// </summary>
    public class FeedResult : ActionResult
    {
        #region Fields

        private readonly SyndicationFeedFormatter feed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedResult"/> class.
        /// </summary>
        /// <param name="feed">The feed.</param>
        public FeedResult(SyndicationFeedFormatter feed)
        {
            this.feed = feed;
        }

        #endregion

        #region Public Properties

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public SyndicationFeedFormatter Feed
        {
            get
            {
                return this.feed;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/rss+xml";

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.feed == null)
            {
                return;
            }

            using (var xmlWriter = new XmlTextWriter(response.Output))
            {
                xmlWriter.Formatting = Formatting.Indented;
                this.feed.WriteTo(xmlWriter);
            }
        }

        #endregion
    }
}