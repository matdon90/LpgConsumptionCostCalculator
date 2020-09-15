using LpgConsumptionCostCalculator.Data.Interfaces;
using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.QueryOptions;
using LpgConsumptionCostCalculator.Web.Behaviors;
using LpgConsumptionCostCalculator.Web.Filters;
using LpgConsumptionCostCalculator.Web.Infrastructure;
using LpgConsumptionCostCalculator.Web.ViewModels;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class FuelReceiptsController : BaseController
    {
        private readonly IFuelReceiptData dbReceipt;
        private readonly ICarData dbCar;

        public FuelReceiptsController(IFuelReceiptData dbReceipt, ICarData dbCar)
        {
            this.dbReceipt = dbReceipt;
            this.dbCar = dbCar;
        }

        // GET: FuelReceipts
        public async Task<ActionResult> Index(int? id, [Form] TableQueryOptions queryOptions, string searchString)
        {
            ViewBag.QueryOptions = queryOptions;
            var start = QueryOptionsCalculator.CalculateStartPage(queryOptions);
            ViewBag.CarId = id;

            if (id!=null)
            {
                var results = await dbReceipt.GetAll();
                var resultsCar = await dbCar.Get(id.GetValueOrDefault());
                ViewBag.CarName = $"{resultsCar.CarProducer} {resultsCar.CarModel}";
                ViewBag.NumberOfEntries = results.Count();
                var receiptViewModels = results.Where(m => m.FueledCarId == id).Select(vm => vm.ToViewModel()).AsQueryable<FuelReceiptViewModel>();
                ViewBag.NumberOfEntries = receiptViewModels.Count();
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToUpper();
                    receiptViewModels = receiptViewModels.Where(r => r.RefuelingDate.ToString().Contains(searchString) || r.PetrolStationName.ToUpper().Contains(searchString)
                    || r.FuelPrice.ToString().Contains(searchString) || r.FuelAmount.ToString().Contains(searchString)
                    || r.FuelConsumption.ToString().Contains(searchString) || r.PriceFor100km.ToString().Contains(searchString));
                }
                queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages(receiptViewModels.Count(),queryOptions.PageSize);
                return View(receiptViewModels.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize).ToList());
            }
            return RedirectToAction("Index", "Cars");
        }

        // GET: FuelReceipts/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var model = await dbReceipt.Get(id);
                if (model == null)
                {
                    Response.StatusCode = 403;
                    return View("NotFound");
                }
                else
                {
                    var viewModel = model.ToViewModel();
                    return PartialView(viewModel);
                }
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }
            
        }

        [Authorize]
        // GET: FuelReceipts/Create
        public async Task<ActionResult> Create(int id)
        {
            ViewBag.CarID = id;
            var resultsCar = await dbCar.Get(id);
            ViewBag.CarName = $"{resultsCar.CarProducer} {resultsCar.CarModel}";
            return View();
        }

        [Authorize]
        [LogUsersActivity]
        // POST: FuelReceipts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FuelReceipt fuelReceipt)
        {
            if (ModelState.IsValid)
            {
                await dbReceipt.Add(fuelReceipt);
                return RedirectToAction("Index", new { id = fuelReceipt.FueledCarId });
            }
            return RedirectToAction("Create", new { id = fuelReceipt.FueledCarId });
        }

        [Authorize]
        // GET: FuelReceipts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await dbReceipt.Get(id);
            var resultsCar = await dbCar.Get(model.FueledCarId);
            ViewBag.CarName = $"{resultsCar.CarProducer} {resultsCar.CarModel}";
            if (model == null)
            {
                return View("NotFound");
            }
            else
            {
                var viewModel = model.ToViewModel();
                return View(viewModel);
            }
        }

        [Authorize]
        [LogUsersActivity]
        // POST: FuelReceipts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FuelReceiptViewModel fuelReceiptViewModel)
        {
            if (ModelState.IsValid)
            {
                var fuelReceipt = fuelReceiptViewModel.ToEntityModel();
                await dbReceipt.Update(fuelReceipt);
                TempData["Message"] = "You have saved the fuel receipt!";
                return RedirectToAction("Index", new { id = fuelReceipt.FueledCarId });
            }
            return RedirectToAction("Edit", new { id = fuelReceiptViewModel.Id });
        }

        [Authorize]
        // GET: FuelReceipts/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (Request.IsAjaxRequest()) 
            { 
                var model = await dbReceipt.Get(id);

                if (model == null)
                {
                    Response.StatusCode = 403;
                    return View("NotFound");
                }
                else
                {
                    var viewModel = model.ToViewModel();
                    return PartialView(viewModel);
                }
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }
        }

        [Authorize]
        [LogUsersActivity]
        // POST: FuelReceipts/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var model = await dbReceipt.Get(id);
            await dbReceipt.Delete(id);
            return RedirectToAction("Index", new { id = model.FueledCarId });
        }
    }
}
