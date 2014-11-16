using System.Web.Optimization;

namespace IAmBacon
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            CssBundles(bundles);
            
            // If you'd like to test the optimization locally,
            // you can use this line to force it.
            ////BundleTable.EnableOptimizations = true;
        }

        public static void CssBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/landingpageCss")
                .Include("~/Content/stylesheets/pages/post/landingpage.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/landingpageIeCss")
                .Include("~/Content/stylesheets/pages/post/landingpage-ie.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/postCss")
                .Include("~/Content/stylesheets/pages/post/post.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/postIeCss")
                .Include("~/Content/stylesheets/pages/post/post-ie.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/categoryCss")
                .Include("~/Content/stylesheets/pages/post/category.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/categoryIeCss")
                .Include("~/Content/stylesheets/pages/post/category-ie.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/tagCss")
                .Include("~/Content/stylesheets/pages/post/tag.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/tagIeCss")
                .Include("~/Content/stylesheets/pages/post/tag-ie.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/homeCss")
                .Include("~/Content/stylesheets/pages/home/home.css", new CssRewriteUrlTransform()));
        }
    }
}