using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class ChartsController : Controller
    {
        private readonly IFuelReceiptData db;

        public ChartsController(IFuelReceiptData db)
        {
            this.db = db;
        }
        // GET: Charts
        public async Task<ActionResult> Chart(int? id, [Form] ChartQueryOptions chartQueryOptions)
        {
            ViewBag.chartQueryOptions = chartQueryOptions;
            var startDate = chartQueryOptions.startingTimeRange;

            if (id!=null)
            {
                var results = await db.GetAll();
                var receiptViewModels = results.Where(vm => vm.FueledCarId == id)
                                        .Where(vm => vm.RefuelingDate >= startDate)
                                        .Select(vm => new FuelReceiptViewModel
                {
                    Id = vm.Id,
                    FueledCarId = vm.FueledCarId,
                    DistanceFromLastRefueling = vm.DistanceFromLastRefueling,
                    Comment = vm.Comment,
                    FuelAmount = vm.FuelAmount,
                    RefuelingDate = vm.RefuelingDate,
                    FuelPrice = vm.FuelPrice,
                    FuelType = vm.FuelType,
                    PetrolStationName = vm.PetrolStationName
                });
                var chartViewModel = new ChartViewModel()
                {
                    lpgConsumptionArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Select(r => decimal.Round(r.FuelConsumption, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    lpgPriceArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Select(r => decimal.Round(r.PriceFor100km, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    lpgDateTimesArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Select(r => r.RefuelingDate.ToString("dd/MM/yyyy")).ToArray(),
                    petrolConsumptionArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.Petrol).Select(r => decimal.Round(r.FuelConsumption, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    petrolPriceArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.Petrol).Select(r => decimal.Round(r.PriceFor100km, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    petrolDateTimesArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.Petrol).Select(r => r.RefuelingDate.ToString("dd/MM/yyyy")).ToArray(),
                    dieselConsumptionArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.Diesel).Select(r => decimal.Round(r.FuelConsumption, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    dieselPriceArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.Diesel).Select(r => decimal.Round(r.PriceFor100km, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    dieselDateTimesArray = receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.Diesel).Select(r => r.RefuelingDate.ToString("dd/MM/yyyy")).ToArray(),

                    averageFuelCons = decimal.Round(receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Average(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero),
                    maxFuelCons = decimal.Round(receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Max(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero),
                    minFuelCons = decimal.Round(receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Min(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero),
                    avgPrice = decimal.Round(receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Average(r => r.PriceFor100km), 2, MidpointRounding.AwayFromZero),
                    totalDistance = decimal.Round(receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Sum(r => r.DistanceFromLastRefueling), 2, MidpointRounding.AwayFromZero),
                    totalPrice = decimal.Round(receiptViewModels.Where(vm => vm.FuelType == TypeOfFuel.LPG).Sum(r => r.FuelAmount * r.FuelPrice), 2, MidpointRounding.AwayFromZero)
                };
                return View(chartViewModel);
            }
            return RedirectToAction("Index", "Cars");

        }
    }
}