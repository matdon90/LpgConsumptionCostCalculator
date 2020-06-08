using System.Web;
using System.Web.Optimization;

namespace LpgConsumptionCostCalculator.Web
{
    public class BundleConfig
    {
        // Aby uzyskać więcej informacji o grupowaniu, odwiedź stronę https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Użyj wersji deweloperskiej biblioteki Modernizr do nauki i opracowywania rozwiązań. Następnie, kiedy wszystko będzie
            // gotowe do produkcji, użyj narzędzia do kompilowania ze strony https://modernizr.com, aby wybrać wyłącznie potrzebne testy.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/sb-admin-2.css"));

            //Create bundel for jQueryUI  
            //js  
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-1.12.1.min.js"));
            //css  
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                   "~/Content/jquery-ui.min.css"));

            //Create bundle for Chart.js

            //js
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                        "~/Scripts/Chart.min.js"));
            //css
            bundles.Add(new StyleBundle("~/Content/csschartjs").Include(
                        "~/Content/Chart.min.css"));

            //Create bundle for sb-admin-2

            //js
            bundles.Add(new ScriptBundle("~/bundles/sbadmin2").Include(
                        "~/Scripts/sb-admin-2.min.js"));

            //Create bundle for jQuery easing

            //js
            bundles.Add(new ScriptBundle("~/bundles/jqueryeasing").Include(
                        "~/Scripts/jquery.easing.min.js"));

            //Bundel for jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));
        }
    }
}
