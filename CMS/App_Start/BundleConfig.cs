using System.Web;
using System.Web.Optimization;

namespace CMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {



            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/areas/admin/content/js/jquery-1.10.2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/areas/admin/content/js/jquery.validate*"));
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/areas/admin/content/js/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/jquery").Include(
            //           "~/content/scripts/jquery-{version}.min.js"));

            //bundles.Add(new ScriptBundle("~/jqueryval").Include(
            //            "~/content/scripts/jquery.validate*"));



            ////کد زیر باعث کمپرس شدنه

            //bundles.Add(new ScriptBundle("~/bundles/Admin/Adminjs").Include(
            //            "~/areas/admin/content/js/modernizr.min.js",
            //            "~/areas/admin/content/js/jquery.min.js",
            //            "~/areas/admin/content/js/bootstrap-rtl.min.js",
            //            "~/areas/admin/content/js/detect.js",
            //            "~/areas/admin/content/js/fastclick.js",
            //            "~/areas/admin/content/js/jquery.slimscroll.js",
            //            "~/areas/admin/content/js/jquery.blockUI.js",
            //            "~/areas/admin/content/js/waves.js",
            //            "~/areas/admin/content/js/wow.min.js",
            //            "~/areas/admin/content/js/jquery.nicescroll.js",
            //            "~/areas/admin/content/js/jquery.scrollTo.min.js",
            //            "~/areas/admin/content/js/jquery.core.js",
            //            "~/areas/admin/content/js/jquery.app.js",
            //            "~/Areas/Admin/Content/js/custome-script.js",
            //            "~/Areas/Admin/Content/plugins/notifyjs/js/notify.js",
            //            "~/Areas/Admin/Content/plugins/notifications/notify-metro.js",
            //            "~/Areas/Admin/content/js/KendoJs/kendo.all.min.js",
            //            "~/Areas/Admin/content/js/KendoJs/kendo.fa-IR.js",
            //            "~/Areas/Admin/content/plugins/bootstrap-sweetalert/sweet-alert.min.js",
            //            "~/Areas/Admin/content/pages/jquery.sweet-alert.init.js"

            //            ));




            //bundles.Add(new StyleBundle("~/bundles/Admin/AdminCss").Include(
            //            "~/areas/admin/content/plugins/morris/morris.css",
            //            "~/areas/admin/content/plugins/bootstrap-sweetalert/sweet-alert.css",
            //            "~/areas/admin/content/css/bootstrap-rtl.min.css",
            //            "~/areas/admin/content/css/core.css",
            //            "~/areas/admin/content/css/components.css",
            //            "~/areas/admin/content/css/icons.css",
            //            "~/areas/admin/content/css/pages.css",
            //            "~/areas/admin/content/css/responsive.css",
            //            "~/Areas/Admin/Content/css/KendoCss/kendo.common-bootstrap.min.css",
            //            "~/Areas/Admin/Content/css/KendoCss/kendo.default.min.css",
            //            "~/Areas/Admin/Content/css/KendoCss/kendo.bootstrap.min.css",
            //            "~/Areas/Admin/Content/css/KendoCss/kendo.rtl.min.css"

            //          ));




        }
    }
}
