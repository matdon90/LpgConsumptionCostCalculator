using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
    }
}