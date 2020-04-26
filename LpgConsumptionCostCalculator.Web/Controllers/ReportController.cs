using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Web.Behaviors;
using LpgConsumptionCostCalculator.Web.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly ICarData dbCar;
        private readonly IFuelReceiptData dbReceipt;

        public ReportController(ICarData dbCar, IFuelReceiptData dbReceipt)
        {
            this.dbCar = dbCar;
            this.dbReceipt = dbReceipt;
        }

        // GET: Raport
        [HttpGet]
        public async Task<ActionResult> Configure(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var model = await dbCar.Get(id);
                if (model == null)
                {
                    Response.StatusCode = 403;
                    return View("NotFound");
                }
                var viewModel = new ReportConfigureViewModel()
                {
                    CarId = model.Id,
                    CarData = $"{model.CarProducer} {model.CarModel}",
                    StartDate = DateTime.Now.AddMonths(-3),
                    EndDate = DateTime.Now
                };
                return PartialView(viewModel);
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportPDF(ReportConfigureViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<FileResult> ExportCSV(ReportConfigureViewModel viewModel)
        {
            var receiptsResults = await dbReceipt.GetAll();

            string fileName = ($"Report {viewModel.CarData} {viewModel.StartDate.ToString("yyyyMMdd")} {viewModel.EndDate.ToString("yyyyMMdd")}.csv").Replace(' ', '_');
            byte[] fileBody = CsvGenerator.CreateCsvBody(viewModel, receiptsResults);

            return File(fileBody, "text/csv", fileName);
        }
    }
}