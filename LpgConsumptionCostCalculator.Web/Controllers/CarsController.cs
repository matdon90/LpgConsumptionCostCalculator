using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.Get(id);
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
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Add(car);
                return RedirectToAction("Details", new { id = car.CarId });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Get(id);

            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Update(car);
                TempData["Message"] = "You have saved the car!";
                return RedirectToAction("Details", new { id = car.CarId });
            }
            return View(car);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.Get(id);

            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}