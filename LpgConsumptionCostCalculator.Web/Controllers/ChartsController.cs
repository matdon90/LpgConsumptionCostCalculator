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
        public async Task<ActionResult> Chart()
        {
            var results = await db.GetAll();
            var receiptViewModels = results.Select(vm => new FuelReceiptViewModel
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
            ViewBag.consumptionList = receiptViewModels.Select(r => r.FuelConsumption).ToList();
            ViewBag.dateList = receiptViewModels.Select(r =>  r.RefuelingDate).ToList();
            return View();
        }
    }
}