using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
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
        public async Task<ActionResult> Index(int? id, [Form] QueryOptions queryOptions, string searchString)
        {
            ViewBag.QueryOptions = queryOptions;
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            ViewBag.CarId = id;
            if (id!=null)
            {
                var results = await db.GetAll();

                var receiptViewModels = results.Where(vm => vm.FueledCarId == id).Select(vm => new FuelReceiptViewModel
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
                }).AsQueryable<FuelReceiptViewModel>();

                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToUpper();
                    receiptViewModels = receiptViewModels.Where(r => r.RefuelingDate.ToString().Contains(searchString) || r.PetrolStationName.ToUpper().Contains(searchString)
                    || r.FuelPrice.ToString().Contains(searchString) || r.FuelAmount.ToString().Contains(searchString)
                    || r.FuelConsumption.ToString().Contains(searchString) || r.PriceFor100km.ToString().Contains(searchString));
                }
                queryOptions.TotalPages = (int)Math.Ceiling((double)receiptViewModels.Count() / queryOptions.PageSize);
                return View(receiptViewModels.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize).ToList());
            }
            return RedirectToAction("Index", "Cars");
        }

        // GET: FuelReceipts/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var model = await db.Get(id);
                if (model == null)
                {
                    Response.StatusCode = 403;
                    return View("NotFound");
                }
                else
                {
                    var viewModel = new FuelReceiptViewModel
                    {
                        Id = model.Id,
                        FueledCarId = model.FueledCarId,
                        DistanceFromLastRefueling = model.DistanceFromLastRefueling,
                        Comment = model.Comment,
                        FuelAmount = model.FuelAmount,
                        RefuelingDate = model.RefuelingDate,
                        FuelPrice = model.FuelPrice,
                        FuelType = model.FuelType,
                        PetrolStationName = model.PetrolStationName
                    };
                    return PartialView(viewModel);
                }
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
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
                return RedirectToAction("Index", new { id = fuelReceipt.FueledCarId });
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
                    Id = model.Id,
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
                    Id = fuelReceiptViewModel.Id,
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
                return RedirectToAction("Index", new { id = fuelReceipt.FueledCarId });
            }
            return View(fuelReceiptViewModel);
        }

        // GET: FuelReceipts/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (Request.IsAjaxRequest()) 
            { 
                var model = await db.Get(id);

                if (model == null)
                {
                    Response.StatusCode = 403;
                    return View("NotFound");
                }
                else
                {
                    var viewModel = new FuelReceiptViewModel
                    {
                        Id = model.Id,
                        FueledCarId = model.FueledCarId,
                        DistanceFromLastRefueling = model.DistanceFromLastRefueling,
                        Comment = model.Comment,
                        FuelAmount = model.FuelAmount,
                        RefuelingDate = model.RefuelingDate,
                        FuelPrice = model.FuelPrice,
                        FuelType = model.FuelType,
                        PetrolStationName = model.PetrolStationName
                    };
                    return PartialView(viewModel);
                }
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }
        }

        // POST: FuelReceipts/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var model = await db.Get(id);
            await db.Delete(id);
            return RedirectToAction("Index", new { id = model.FueledCarId });
        }
    }
}
