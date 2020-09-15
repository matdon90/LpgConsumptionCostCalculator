using LpgConsumptionCostCalculator.Web.ViewModels;
using LpgConsumptionCostCalculator.Web.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;
using LpgConsumptionCostCalculator.Data.Interfaces;
using LpgConsumptionCostCalculator.Data.Enums;
using LpgConsumptionCostCalculator.Data.QueryOptions;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class ChartsController : BaseController
    {
        private readonly IFuelReceiptData db;
        private readonly ICarData dbCar;

        public ChartsController(IFuelReceiptData db,ICarData dbCar)
        {
            this.db = db;
            this.dbCar = dbCar;
        }
        // GET: Charts
        public async Task<ActionResult> Chart(int? id, [Form] ChartQueryOptions chartQueryOptions)
        {
            ViewBag.chartQueryOptions = chartQueryOptions;
            
            if (id!=null)
            {
                var results = await db.GetAll();
                var carResults = await dbCar.Get(id.GetValueOrDefault());
                if (results != null)
                {
                    var lastFuelingReceipt = results.Where(r => r.FueledCarId == id).Where(r => r.FuelType == TypeOfFuel.LPG);
                    if (lastFuelingReceipt.Count() == 0)
                    {
                        return RedirectToAction("Index", "FuelReceipts", new { id });
                    }
                    var lastFueling = lastFuelingReceipt.OrderByDescending(r => r.RefuelingDate).First().RefuelingDate;
                    var startDate = chartQueryOptions.startingTimeRange > lastFueling ? lastFueling : chartQueryOptions.startingTimeRange;
                    ViewBag.startingdate = startDate;
                    var receiptViewModels = results.Where(vm => vm.FueledCarId == id).Where(vm => vm.RefuelingDate >= startDate).Select(vm => vm.ToViewModel());
                    var chartViewModel = new ChartViewModel()
                    {
                        carId = carResults.Id,
                        carData = $"{carResults.CarProducer} {carResults.CarModel}",
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
                else
                {
                    return RedirectToAction("Index", "FuelReceipts", new { id });
                }
            }
            return RedirectToAction("Index", "Cars");

        }
    }
}