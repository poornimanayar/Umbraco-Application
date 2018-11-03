namespace Umbraco_Application
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/assets/scripts/site.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/assets/styles/bootstrap.css",
                      "~/assets/styles/site.css"));

            bundles.Add(new StyleBundle("~/bundles/brand1").Include(
                "~/assets/brand1/styles.css"));

            bundles.Add(new StyleBundle("~/bundles/brand2").Include(
                "~/assets/brand2/styles.css"));

            bundles.Add(new StyleBundle("~/bundles/resellerbrand").Include(
                "~/assets/resellerbrand/styles.css"));
        }
    }
}
