using LpgConsumptionCostCalculator.Web.App_Code;
using LpgConsumptionCostCalculator.Web.App_Start;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LpgConsumptionCostCalculator.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ContainerConfig.RegisterContainer(GlobalConfiguration.Configuration);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "name";
            AutoMapperConfig.AutoMapperRegister();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MultiLanguageViewEngine());
        }
    }
}
