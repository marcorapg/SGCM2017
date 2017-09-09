using System.Web;
using System.Web.Optimization;

namespace SGCM
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jqueryui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
                        "~/Scripts/jquery.mask*"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                        "~/Scripts/fullcalendar.min.js",
                        "~/Scripts/gcal.min.js",
                        "~/Scripts/locale-all.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));



            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/jqueryui").Include(
                      "~/Content/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/customcss").Include(
                      "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/fullcalendar").Include(
                      "~/Content/fullcalendar.min.css",
                      "~/Content/fullcalendar.print.min.css"));



            BundleTable.EnableOptimizations = false;
        }
    }
}