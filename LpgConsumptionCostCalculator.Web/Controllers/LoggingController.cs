using LpgConsumptionCostCalculator.Web.Behaviors;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Dynamic;
using System;
using LpgConsumptionCostCalculator.Data.Interfaces;
using LpgConsumptionCostCalculator.Data.QueryOptions;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class LoggingController : BaseController
    {
        private readonly ILogData _db;

        public LoggingController(ILogData db)
        {
            _db = db;
        }

        [Authorize(Users = "Mateusz Donhefner")]
        [HttpGet]
        public async Task<ActionResult> Logs([Form] TableQueryOptions queryOptions, string searchString)
        {
            ViewBag.QueryOptions = queryOptions;
            var start = QueryOptionsCalculator.CalculateStartPage(queryOptions);
            var logsModel = await _db.GetUsersData();
            ViewBag.NumberOfEntries = logsModel.Count();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                logsModel = logsModel.Where(l => l.UserName.ToUpper().Contains(searchString) || l.LogTime.ToString().Contains(searchString) || l.LogMessage.ToUpper().Contains(searchString));
            }
            queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages(logsModel.Count(), queryOptions.PageSize);

            return View(logsModel.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize).ToList());
        }
    }
}