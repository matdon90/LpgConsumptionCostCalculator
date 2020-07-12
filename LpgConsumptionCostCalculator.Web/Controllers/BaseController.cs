using LpgConsumptionCostCalculator.Web.Helper;
using System;
using System.Web;
using System.Web.Mvc;
using LpgConsumptionCostCalculator.Web.App_Code;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class BaseController : Controller
    {
        private static string _cookieLangName = "LangForMultiLanguage";

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureOnCookie = GetCultureOnCookie(filterContext.HttpContext.Request);
            string cultureOnURL = filterContext.RouteData.Values.ContainsKey("lang")
                ? filterContext.RouteData.Values["lang"].ToString()
                : GlobalHelper.DefaultCulture;
            string culture = (cultureOnCookie == string.Empty)
                ? (filterContext.RouteData.Values["lang"].ToString())
                : cultureOnCookie;

            if (cultureOnURL != culture)
            {
                filterContext.HttpContext.Response.RedirectToRoute("LocalizedDefault",
                new
                {
                    lang = culture,
                    controller = filterContext.RouteData.Values["controller"],
                    action = filterContext.RouteData.Values["action"],
                    id = filterContext.RouteData.Values["id"]
                });
                return;
            }

            SetCurrentCultureOnThread(culture);

            if (culture != MultiLanguageViewEngine.CurrentCulture)
            {
                (ViewEngines.Engines[0] as MultiLanguageViewEngine).SetCurrentCulture(culture);
            }

            base.OnActionExecuting(filterContext);
        }

        private static void SetCurrentCultureOnThread(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = GlobalHelper.DefaultCulture;
            var cultureInfo = new System.Globalization.CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        public static String GetCultureOnCookie(HttpRequestBase request)
        {
            var cookie = request.Cookies[_cookieLangName];
            string culture = string.Empty;
            if (cookie != null)
            {
                culture = cookie.Value;
            }
            return culture;
        }

    }
}