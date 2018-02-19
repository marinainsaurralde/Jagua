using System.Web.Optimization;

namespace Jagua.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(
                "~/Content/assets/css/bootstrap.min.css",
                "~/Content/assets/css/light-bootstrap-dashboard.css",
                "~/Content/assets/css/demo.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include(
                "~/Content/assets/js/core/jquery.3.2.1.min.js",
                "~/Content/assets/js/core/popper.min.js",
                "~/Content/assets/js/core/bootstrap.min.js",
                "~/Content/assets/js/plugins/bootstrap-switch.js",
                "~/Content/assets/js/plugins/bootstrap-switch.js",
                "~/Content/assets/js/plugins/chartist.min.js",
                "~/Content/assets/js/plugins/bootstrap-notify.js",
                "~/Content/assets/js/light-bootstrap-dashboard.js",
                "~/Content/assets/js/demo.js"
                ));
        }

    }
}