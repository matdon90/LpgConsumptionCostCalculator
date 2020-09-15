using LpgConsumptionCostCalculator.Data.Enums;
using LpgConsumptionCostCalculator.Data.Interfaces;
using LpgConsumptionCostCalculator.Web.Behaviors;
using LpgConsumptionCostCalculator.Web.Filters;
using LpgConsumptionCostCalculator.Web.Infrastructure;
using LpgConsumptionCostCalculator.Web.ViewModels;
using Rotativa;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class ReportController : BaseController
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

        [Authorize]
        [LogUsersActivity]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExportPDF(ReportConfigureViewModel viewModel)
        {
            var receiptsModel = await dbReceipt.GetAll();
            var receiptsViewModel = receiptsModel.Where(m => m.FueledCarId == viewModel.CarId)
                        .Where(m => m.RefuelingDate >= viewModel.StartDate)
                        .Where(m => m.RefuelingDate <= viewModel.EndDate)
                        .Select(vm => vm.ToViewModel());

            var averageFuelCons = decimal.Round(receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Average(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.');
            var maxFuelCons = decimal.Round(receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Max(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.');
            var minFuelCons = decimal.Round(receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Min(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.');
            var avgPrice = decimal.Round(receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Average(r => r.PriceFor100km), 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.');
            var totalDistance = decimal.Round(receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Sum(r => r.DistanceFromLastRefueling), 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.');
            var totalPrice = decimal.Round(receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Sum(r => r.FuelAmount * r.FuelPrice), 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.');
            var lpgConsumptionArray = receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Select(r => decimal.Round(r.FuelConsumption, 2, MidpointRounding.AwayFromZero)).ToArray();
            var lpgDateTimesArray = receiptsViewModel.Where(m => m.FuelType == TypeOfFuel.LPG).Select(r => r.RefuelingDate.ToString("dd.MM.yyyy")).ToArray();

            var exportPdfViewModel = receiptsViewModel.Select(vm => new ExportPdfViewModel
            {
                ReportAuthor = HttpContext.User.Identity.IsAuthenticated ? HttpContext.User.Identity.Name : "Anonymous",
                CarData = viewModel.CarData.ToUpper(),
                RefuelingDate = vm.RefuelingDate.ToString("dd.MM.yyyy"),
                PetrolStationName = vm.PetrolStationName,
                FuelType = vm.FuelType.ToString(),
                FuelPrice = decimal.Round(vm.FuelPrice, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                FuelAmount = decimal.Round(vm.FuelAmount, 2, MidpointRounding.AwayFromZero).ToString().Replace(',', '.'),
                DistanceFromLastRefueling = decimal.Round(vm.DistanceFromLastRefueling, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                FuelConsumption = decimal.Round(vm.FuelConsumption, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                PriceFor100km = decimal.Round(vm.PriceFor100km, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                AverageFuelCons = averageFuelCons,
                MaxFuelCons = maxFuelCons,
                MinFuelCons = minFuelCons,
                AvgPrice = avgPrice,
                TotalDistance = totalDistance,
                TotalPrice = totalPrice,
                LpgConsumptionArray = lpgConsumptionArray,
                LpgDateTimesArray = lpgDateTimesArray
            });

            string fileName = ($"Report {viewModel.CarData} {viewModel.StartDate.ToString("yyyyMMdd")} {viewModel.EndDate.ToString("yyyyMMdd")}.pdf").Replace(' ', '_');
            string footer = "--footer-center \"Report for " + viewModel.CarData + " from " + viewModel.StartDate.ToString("dd.MM.yyyy") + " to " + viewModel.EndDate.ToString("dd.MM.yyyy")
                + "  Page: [page]/[toPage]\"" + " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            return new PartialViewAsPdf("ExportPDF", exportPdfViewModel)
            {
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                FileName = fileName,
                CustomSwitches = footer
            };
        }

        [Authorize]
        [LogUsersActivity]
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