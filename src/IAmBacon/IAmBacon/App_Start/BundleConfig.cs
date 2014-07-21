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
                .Include("~/Content/stylesheets/landingpage.css"));

            bundles.Add(new StyleBundle("~/bundles/landingpageIeCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/landingpage-ie.css"));

            bundles.Add(new StyleBundle("~/bundles/postCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/post.css"));

            bundles.Add(new StyleBundle("~/bundles/postIeCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/post-ie.css"));

            bundles.Add(new StyleBundle("~/bundles/categoryCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/category.css"));

            bundles.Add(new StyleBundle("~/bundles/categoryIeCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/category-ie.css"));

            bundles.Add(new StyleBundle("~/bundles/tagCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/tag.css"));

            bundles.Add(new StyleBundle("~/bundles/tagIeCss")
                .Include("~/Content/css/modules/normalize.css")
                .Include("~/Content/stylesheets/tag-ie.css"));

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

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
        }
    }
}