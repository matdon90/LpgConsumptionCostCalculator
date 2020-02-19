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
    public class FuelReceiptsController : Controller
    {
        private readonly IFuelReceiptData db;

        public FuelReceiptsController(IFuelReceiptData db)
        {
            this.db = db;
        }
        // GET: FuelReceipts
        public async Task<ActionResult> Index(int? id)
        {
            ViewBag.CarId = id;
            if (id!=null)
            {
                var results = await db.GetAll();
                IEnumerable<FuelReceiptViewModel> receiptViewModels = results.Where(vm => vm.FueledCarId == id).Select(vm => new FuelReceiptViewModel
                {
                    FuelReceiptId = vm.FuelReceiptId,
                    FueledCarId = vm.FueledCarId,
                    DistanceFromLastRefueling = vm.DistanceFromLastRefueling,
                    Comment = vm.Comment,
                    FuelAmount = vm.FuelAmount,
                    RefuelingDate = vm.RefuelingDate,
                    FuelPrice = vm.FuelPrice,
                    FuelType = vm.FuelType,
                    PetrolStationName = vm.PetrolStationName
                }).ToList();
                return View(receiptViewModels);
            }
            return RedirectToAction("Index", "Cars");
        }

        // GET: FuelReceipts/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            else
            {
                var viewModel = new FuelReceiptViewModel
                {
                    FuelReceiptId = model.FuelReceiptId,
                    FueledCarId = model.FueledCarId,
                    DistanceFromLastRefueling = model.DistanceFromLastRefueling,
                    Comment = model.Comment,
                    FuelAmount = model.FuelAmount,
                    RefuelingDate = model.RefuelingDate,
                    FuelPrice = model.FuelPrice,
                    FuelType = model.FuelType,
                    PetrolStationName = model.PetrolStationName
                };
                return View(viewModel);
            }
        }

        // GET: FuelReceipts/Create
        public ActionResult Create(int id)
        {
            ViewBag.CarID = id;
            return View();
        }

        // POST: FuelReceipts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FuelReceipt fuelReceipt)
        {
            if (ModelState.IsValid)
            {
                await db.Add(fuelReceipt);
                return RedirectToAction("Details", new { id = fuelReceipt.FuelReceiptId });
            }
            return View();
        }

        // GET: FuelReceipts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            else
            {
                var viewModel = new FuelReceiptViewModel()
                {
                    FuelReceiptId = model.FuelReceiptId,
                    FueledCarId = model.FueledCarId,
                    DistanceFromLastRefueling = model.DistanceFromLastRefueling,
                    Comment = model.Comment,
                    FuelAmount = model.FuelAmount,
                    RefuelingDate = model.RefuelingDate,
                    FuelPrice = model.FuelPrice,
                    FuelType = model.FuelType,
                    PetrolStationName = model.PetrolStationName
                };
                return View(viewModel);
            }
        }

        // POST: FuelReceipts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FuelReceiptViewModel fuelReceiptViewModel)
        {
            if (ModelState.IsValid)
            {
                var fuelReceipt = new FuelReceipt()
                {
                    FuelReceiptId = fuelReceiptViewModel.FuelReceiptId,
                    FueledCarId = fuelReceiptViewModel.FueledCarId,
                    DistanceFromLastRefueling = fuelReceiptViewModel.DistanceFromLastRefueling,
                    Comment = fuelReceiptViewModel.Comment,
                    FuelAmount = fuelReceiptViewModel.FuelAmount,
                    RefuelingDate = fuelReceiptViewModel.RefuelingDate,
                    FuelPrice = fuelReceiptViewModel.FuelPrice,
                    FuelType = fuelReceiptViewModel.FuelType,
                    PetrolStationName = fuelReceiptViewModel.PetrolStationName
                };
                await db.Update(fuelReceipt);
                TempData["Message"] = "You have saved the fuel receipt!";
                return RedirectToAction("Details", "FuelReceipts", new { id = fuelReceiptViewModel.FuelReceiptId });
            }
            return View(fuelReceiptViewModel);
        }

        // GET: FuelReceipts/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await db.Get(id);

            if (model == null)
            {
                return View("NotFound");
            }
            else
            {
                var viewModel = new FuelReceiptViewModel
                {
                    FuelReceiptId = model.FuelReceiptId,
                    FueledCarId = model.FueledCarId,
                    DistanceFromLastRefueling = model.DistanceFromLastRefueling,
                    Comment = model.Comment,
                    FuelAmount = model.FuelAmount,
                    RefuelingDate = model.RefuelingDate,
                    FuelPrice = model.FuelPrice,
                    FuelType = model.FuelType,
                    PetrolStationName = model.PetrolStationName
                };
                return View(viewModel);
            }
        }

        // POST: FuelReceipts/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var model = await db.Get(id);
            await db.Delete(id);
            return RedirectToAction("Index", "FuelReceipts", new { id = model.FuelReceiptId });
        }
    }
}
