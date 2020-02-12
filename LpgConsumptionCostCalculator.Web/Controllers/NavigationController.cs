using LpgConsumptionCostCalculator.Data.Services;
using LpgConsumptionCostCalculator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ICarData db;
        public NavigationController(ICarData db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult Menu()
        {
            IEnumerable<MenuViewModel> menuViewModels = db.GetAll().Select(vm => new MenuViewModel
            {
                CarId = vm.CarId,
                CarModel = vm.CarModel,
                CarProducer = vm.CarProducer,
            }).ToList();

            return PartialView("_Navigation", menuViewModels);
        }
    }
}