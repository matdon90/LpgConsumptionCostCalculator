using LpgConsumptionCostCalculator.Data.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class LoggingController : Controller
    {
        private readonly ILoginData _db;

        public LoggingController(ILoginData db)
        {
            _db = db;
        }

        [Authorize(Users = "Mateusz Donhefner")]
        public async Task<ActionResult> Logs()
        {
            var logs = await _db.GetUsersData();

            return View(logs);
        }
    }
}