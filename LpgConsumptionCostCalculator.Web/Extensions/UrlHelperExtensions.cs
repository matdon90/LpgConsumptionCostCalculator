using System.Web;
using System.IO;
using System.Web.Mvc;
using LpgConsumptionCostCalculator.Web.Helper;

namespace LpgConsumptionCostCalculator.Web.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetImage(this UrlHelper helper, string imageFileName, bool localizable = true)
        {
            string strUrlPath, strFilePath = string.Empty;
            if (localizable)
            {
                /* Search result in current culture folder */
                strUrlPath = string.Format("/Content/Images/{0}/{1}", GlobalHelper.CurrentCulture, imageFileName);
                strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
                if (!File.Exists(strFilePath))
                {   /* Search result in default culture folder */
                    strUrlPath = string.Format("/Content/{0}/Images/{1}", GlobalHelper.DefaultCulture, imageFileName);
                }
                return strUrlPath;
            }
            strUrlPath = string.Format("/Content/Images/{0}", imageFileName);
            strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
            if (File.Exists(strFilePath))
            {   /* search result in root directory folder */
                return strUrlPath;
            }

            return strUrlPath;
        }
        public static string GetScript(this UrlHelper helper, string scriptFileName, bool localizable = true)
        {
            string strUrlPath, strFilePath = string.Empty;
            if (localizable)
            {
                /* Search result in current culture folder */
                strUrlPath = string.Format("/Content/Scripts/{0}/{1}", GlobalHelper.CurrentCulture, scriptFileName);
                strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
                if (!File.Exists(strFilePath))
                {   /* Search result in default culture folder */
                    strUrlPath = string.Format("/Content/{0}/Scripts/{1}", GlobalHelper.DefaultCulture, scriptFileName);
                }
                return strUrlPath;
            }

            strUrlPath = string.Format("/Content/Scripts/{0}", scriptFileName);
            strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
            if (File.Exists(strFilePath))
            {   /* search result in root directory folder */
                return strUrlPath;
            }

            return strUrlPath;
        }
    }
}