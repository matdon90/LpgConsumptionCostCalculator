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
    public class CarsController : BaseController
    {
        private readonly ICarData db;
        public CarsController(ICarData db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult> Index([Form] TableQueryOptions queryOptions, string searchString)
        {
            ViewBag.QueryOptions = queryOptions;
            var start = QueryOptionsCalculator.CalculateStartPage(queryOptions);
            var model = await db.GetAll();
            ViewBag.NumberOfEntries = model.Count();
            var viewModels = model.Select(m => m.ToViewModel());
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                viewModels = viewModels.Where(c => c.CarModel.ToUpper().Contains(searchString) || c.CarProducer.ToUpper().Contains(searchString) 
                || c.CarProductionYear.ToString().Contains(searchString) || c.LpgInstallationModel.ToUpper().Contains(searchString)
                || c.LpgInstallationProducer.ToUpper().Contains(searchString));
            }
            queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages(viewModels.Count(), queryOptions.PageSize);
            return View(viewModels.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize).ToList());
        }

        [HttpGet]
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
                var viewModel = model.ToViewModel();
                return PartialView(viewModel);
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [LogUsersActivity]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await db.Add(car);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            var viewModel = model.ToViewModel();
            return View(viewModel);
        }

        [Authorize]
        [LogUsersActivity]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                var car = carViewModel.ToEntityModel();
                await db.Update(car);
                TempData["Message"] = "You have saved the car!";
                return RedirectToAction("Index");
            }
            return View(carViewModel);
        }

        [Authorize]
        [HttpGet]
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
                var viewModel = model.ToViewModel();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection form)
        {
            await db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}