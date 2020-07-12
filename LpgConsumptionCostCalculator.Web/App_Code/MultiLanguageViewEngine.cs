using LpgConsumptionCostCalculator.Web.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.App_Code
{
    public class MultiLanguageViewEngine : RazorViewEngine
    {
        private static string _currentCulture = GlobalHelper.CurrentCulture;

        public MultiLanguageViewEngine() : this(GlobalHelper.CurrentCulture)
        {
        }

        public MultiLanguageViewEngine(string lang)
        {
            SetCurrentCulture(lang);
        }

        public void SetCurrentCulture(string lang)
        {
            _currentCulture = lang;
            ICollection<string> arViewLocationFormats =
                 new string[] { "~/Views/{1}/" + lang + "/{0}.cshtml" };
            ICollection<string> arBaseViewLocationFormats = new string[] {
                @"~/Views/{1}/{0}.cshtml",
                @"~/Views/Shared/{0}.cshtml"};
            this.ViewLocationFormats = arViewLocationFormats.Concat(arBaseViewLocationFormats).ToArray();
        }

        public static string CurrentCulture
        {
            get { return _currentCulture; }
        }
    }
}