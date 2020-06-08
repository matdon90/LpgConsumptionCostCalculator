using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Web.Behaviors;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarData db;
        public CarsController(ICarData db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult> Index([Form] QueryOptions queryOptions, string searchString)
        {
            ViewBag.QueryOptions = queryOptions;
            var start = QueryOptionsCalculator.CalculateStartPage(queryOptions);
            var model = await db.GetAll();
            ViewBag.NumberOfEntries = model.Count();
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                model = model.Where(c => c.CarModel.ToUpper().Contains(searchString) || c.CarProducer.ToUpper().Contains(searchString) 
                || c.CarProductionYear.ToString().Contains(searchString) || c.LpgInstallationModel.ToUpper().Contains(searchString)
                || c.LpgInstallationProducer.ToUpper().Contains(searchString));
            }
            queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages(model.Count(), queryOptions.PageSize);
            return View(model.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize).ToList());
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
                return PartialView(model);
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }
        }

        //[Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //[Authorize]
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

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                await db.Update(car);
                TempData["Message"] = "You have saved the car!";
                return RedirectToAction("Index");
            }
            return View(car);
        }

        //[Authorize]
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
                return PartialView(model);
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }      
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection form)
        {
            await db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}