using System.Web;
using System.Web.Optimization;

namespace ShoesProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/js/vendor/modernizr-2.6.2.min.js",
                        "~/Scripts/js/vendor/jquery-1.11.3.min.js",
                        "~/Scripts/js/jquery.fancybox.js",
                        "~/Scripts/js/jquery.bxslider.min.js",
                        "~/Scripts/js/jquery.meanmenu.js",
                        "~/Scripts/js/owl.carousel.min.js",
                        "~/Scripts/js/jquery.nivo.slider.js",
                        "~/Scripts/js/jqueryui.js",
                        "~/Scripts/js/bootstrap.min.js",
                        "~/Scripts/js/wow.js",
                        "~/Scripts/js/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/jquery.fancybox.css",
                      "~/Content/css/jquery.bxslider.css",
                      "~/Content/css/meanmenu.min.css",
                      "~/Content/css/jquery-ui-slider.css",
                      "~/Content/css/nivo-slider.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/owl.theme.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/fonts/fonts/font-awesome.min.css",
                      "~/Content/css/normalize.css",
                      "~/Content/css/main.css",
                      "~/Content/style.css",
                      "~/Content/css/responsive.css",
                      "~/Content/css/ie.css"));
        }
    }
}
