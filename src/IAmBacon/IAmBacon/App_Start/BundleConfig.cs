using System.Web.Optimization;

namespace IAmBacon
{
    using BundleTransformer.Core.Orderers;
    using BundleTransformer.Core.Transformers;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            CssBundles(bundles);
            LessBundles(bundles);
            ScriptBundles(bundles);

            // If you'd like to test the optimization locally,
            // you can use this line to force it.
            ////BundleTable.EnableOptimizations = true;
        }

        public static void CssBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/landingpageCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/pages/post/landingpage.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/landingpageIeCss")
                .Include("~/Content/css/modules/normalize.css")
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

        public static void LessBundles(BundleCollection bundles)
        {
            var cssTransformer = new CssTransformer();
            var nullOrderer = new NullOrderer();

            var iambacon = new Bundle("~/bundles/iambacon")
                .Include("~/Content/stylesheets/pages/home/home.css");

            iambacon.Transforms.Add(cssTransformer);
            iambacon.Transforms.Add(new CssMinify());
            iambacon.Orderer = nullOrderer;
            
            bundles.Add(iambacon);
        }
        
        public static void ScriptBundles(BundleCollection bundles)
        {
            ////bundles.UseCdn = true;

            ////var jquery =
            ////    new ScriptBundle("~/bundles/jquery",
            ////    "http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js")
            ////        .Include("~/Scripts/jquery-1.9.0.js");

            ////jquery.CdnFallbackExpression = "window.jQuery";

            ////bundles.Add(jquery);

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui*"));

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
        }
    }
}