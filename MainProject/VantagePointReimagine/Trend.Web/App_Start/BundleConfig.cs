using System.Web;
using System.Web.Optimization;

namespace Trend.Web
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

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                       "~/Scripts/moment.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            //           "~/Scripts/bootstrap-datetimepicker.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                       "~/Scripts/bootstrap-material-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                    //  "~/Scripts/SignalR.StockTicker.js",
                       "~/Scripts/jquery.signalR-2.2.0.js",
                       "~/SignalRChart/SignalR.Chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/material").Include(
                    "~/Scripts/material/ripples.min.js",
                     "~/Scripts/material/material.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                       "~/Scripts/Chart.js"));

            //bundles.Add(new ScriptBundle("~/bundles/chart").Include(
            //          "~/Scripts/Flot/jquery.flot.js"));



            //Change
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-material-datetimepicker.css",
                      "~/Content/material/roboto.min.css",
                     "~/Content/material/material.min.css",
                     "~/Content/material/ripples.min.css"));


            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.min.css",
            //          "~/Content/site.css",
            //         // "~/Content/StockTicker.css",
            //         // "~/Content/dashboard.css",
            //          "~/Content/bootstrap-material-datetimepicker.css"));

            //bundles.Add(new StyleBundle("~/Content/material").Include(
            //        "~/Content/material/roboto.min.css",
            //        "~/Content/material/material.min.css",
            //        "~/Content/material/ripples.min.css"));

            //bundles.Add(new StyleBundle("~/Content/material").Include(
            //       "~/Content/material/roboto.css",
            //       "~/Content/material/material.css",
            //       "~/Content/material/ripples.css"));

            //  BundleTable.EnableOptimizations = true;

        }
    }
}
