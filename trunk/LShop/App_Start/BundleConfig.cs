using System.Web;
using System.Web.Optimization;

namespace LShop
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/cookie").Include(
                        "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/input.js",
                        "~/Scripts/changeSkin.js"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                        "~/Scripts/ad.js",
                        "~/Scripts/imgSlide.js",
                        "~/Scripts/imgHover.js",
                        "~/Scripts/tooltip.js"));

            bundles.Add(new ScriptBundle("~/bundles/detail").Include(
                        "~/Scripts/jquery.zoom.js",
                        "~/Scripts/use_jqzoom.js",
                        "~/Scripts/jquery.thickbox.js",
                        "~/Scripts/switchimg.js",
                        "~/Scripts/tab.js",
                        "~/Scripts/switchcolor.js",
                        "~/Scripts/start.js",
                        "~/Scripts/finish.js",
                        "~/Scripts/sizeandprice.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                        "~/Scripts/jquery.easyui.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                         "~/Content/reset.css",
                         "~/Content/main.css",
                         "~/Content/skin/skin_0.css"));

            bundles.Add(new StyleBundle("~/Content/AdminCss").Include(
                         "~/Content/reset.css",
                         "~/Content/main.css",
                         "~/Content/icon.css",
                         "~/Content/Default/easyui.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
