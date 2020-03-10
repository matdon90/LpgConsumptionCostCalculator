using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<ActionResult> Chart(int? id)
        {
            if (id!=null)
            {
                var results = await db.GetAll();
                var receiptViewModels = results.Where(vm => vm.FueledCarId == id).Where(vm => vm.FuelType == TypeOfFuel.LPG).Select(vm => new FuelReceiptViewModel
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
                    consumptionArray = receiptViewModels.Select(r => decimal.Round(r.FuelConsumption, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    priceArray = receiptViewModels.Select(r => decimal.Round(r.PriceFor100km, 2, MidpointRounding.AwayFromZero)).ToArray(),
                    dateTimesArray = receiptViewModels.Select(r => r.RefuelingDate.ToString("dd/MM/yyyy")).ToArray(),
                    averageFuelCons = decimal.Round(receiptViewModels.Average(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero),
                    maxFuelCons = decimal.Round(receiptViewModels.Max(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero),
                    minFuelCons = decimal.Round(receiptViewModels.Min(r => r.FuelConsumption), 2, MidpointRounding.AwayFromZero),
                    avgPrice = decimal.Round(receiptViewModels.Average(r => r.PriceFor100km), 2, MidpointRounding.AwayFromZero),
                    totalDistance = decimal.Round(receiptViewModels.Sum(r => r.DistanceFromLastRefueling), 2, MidpointRounding.AwayFromZero),
                    totalPrice = decimal.Round(receiptViewModels.Average(r => r.DistanceFromLastRefueling * r.FuelPrice), 2, MidpointRounding.AwayFromZero)
                };
                return View(chartViewModel);
            }
            return RedirectToAction("Index", "Cars");

        }
    }
}