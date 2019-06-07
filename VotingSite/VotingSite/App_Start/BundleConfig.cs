
using System.Web;
using System.Web.Optimization;

namespace VotingSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    "~/Scripts/bootstrap.js",
            //    "~/Scripts/fontawesome.js",
            //    "~/Scripts/solid.js"));

            // Bootstrap core CSS -- REMOVED. They totally wrote their own [S]CSS,
            // and since I'm trying to use as much of it as I can, I've removed 
            // Bootstrap to make sure I am only using their markup. -SKF

            bundles.Add(new StyleBundle("~/Content/css").Include(
                //"~/Content/css/bootstrap.css",
                "~/Content/css/index.css",
                "~/Content/css/toolbars.css",
                "~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/landAndVote").Include(
                "~/Content/css/index.css",
                "~/Content/css/toolbars.css",
                "~/Content/css/landAndVoteLayout.css",
                "~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/loginPage").Include(
                "~/Content/css/index.css",
                "~/Content/css/toolbars.css",
                "~/Content/css/loginPage.css",
                "~/Content/css/site.css"));
        }
    }
}
