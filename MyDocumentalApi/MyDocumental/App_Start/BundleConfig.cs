using System.Web;
using System.Web.Optimization;

namespace MyDocumental
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                      "~/Scripts/jquery.signalR-2.1.2.min.js")
                      );

            bundles.Add(new ScriptBundle("~/bundles/libs")
                      .Include("~/bower_components/lodash/dist/lodash.js")
                      );

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/bower_components/angular/angular.js")
                      .Include("~/bower_components/angular-route/angular-route.js")
                      .Include("~/bower_components/angular-resource/angular-resource.js")
                      .Include("~/bower_components/angular-cookies/angular-cookies.js")
                      .Include("~/bower_components/angular-translate/angular-translate.js")
                      .Include("~/bower_components/angular-translate-loader-url/angular-translate-loader-url.js")
                      );

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/Scripts/app","*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
