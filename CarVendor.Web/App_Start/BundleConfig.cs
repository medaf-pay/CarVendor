using System.Web;
using System.Web.Optimization;

namespace CarVendor.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Content/js/jquery.min.js",
                "~/Content/js/angular.min.js",
                "~/Content/js/Module.js",
                "~/Content/js/CartCTR.js",
                "~/Content/js/camera.js",
                "~/Content/js/jquery.equalheights.js",
                "~/Content/js/script.js",
                "~/Content/js/jquery.tabs.js",
                "~/Content/js/wow/wow.js",
                "~/Content/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/css/grid.css",
                      "~/Content/css/search.css",
                      "~/Content/css/camera.css",
                      "~/Content/booking/css/booking.css",
                      "~/Content/css/style.css"));

        }
    }
}
