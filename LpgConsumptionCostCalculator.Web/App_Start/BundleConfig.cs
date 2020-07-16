using System.Web.Optimization;

namespace LpgConsumptionCostCalculator.Web
{
    public class BundleConfig
    {
        // Aby uzyskać więcej informacji o grupowaniu, odwiedź stronę https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-{version}.js"));

            // Użyj wersji deweloperskiej biblioteki Modernizr do nauki i opracowywania rozwiązań. Następnie, kiedy wszystko będzie
            // gotowe do produkcji, użyj narzędzia do kompilowania ze strony https://modernizr.com, aby wybrać wyłącznie potrzebne testy.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                      "~/Content/Styles/bootstrap.min.css",
                      "~/Content/Styles/site.css",
                      "~/Content/Styles/sb-admin-2.css",
                      "~/Content/Styles/site.css")
                .Include(
                      "~/Content/Style/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform()));

            //Create bundel for jQueryUI  
            //js  
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Content/Scripts/jquery-ui-1.12.1.min.js"));
            //css  
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                   "~/Content/Styles/jquery-ui.min.css"));

            //Create bundle for Chart.js

            //js
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                        "~/Content/Scripts/Chart.min.js"));
            //css
            bundles.Add(new StyleBundle("~/Content/csschartjs").Include(
                        "~/Content/Style/Chart.min.css"));

            //Create bundle for sb-admin-2

            //js
            bundles.Add(new ScriptBundle("~/bundles/sbadmin2").Include(
                        "~/Content/Scripts/sb-admin-2.min.js"));

            //Create bundle for jQuery easing

            //js
            bundles.Add(new ScriptBundle("~/bundles/jqueryeasing").Include(
                        "~/Content/Scripts/jquery.easing.min.js"));

            //Bundel for jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate.js",
                        "~/Content/Scripts/jquery.validate.unobtrusive.min.js"));

            //Bundle for MultiLanguage
            bundles.Add(new ScriptBundle("~/bundles/multiLang").Include(
                "~/Content/Scripts/multiLang.js"));
        }
    }
}
