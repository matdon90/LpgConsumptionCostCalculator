using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index()
        {
            var model = await db.GetAll();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var model = await db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                car.CarId = 10;
                await db.Add(car);
                return RedirectToAction("Details", new { id = car.CarId });
            }
            return View();
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                await db.Update(car);
                TempData["Message"] = "You have saved the car!";
                return RedirectToAction("Details", new { id = car.CarId });
            }
            return View(car);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await db.Get(id);

            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection form)
        {
            await db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}