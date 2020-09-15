using LpgConsumptionCostCalculator.Web.ViewModels;
using LpgConsumptionCostCalculator.Web.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using LpgConsumptionCostCalculator.Data.Interfaces;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ICarData db;
        public NavigationController(ICarData db)
        {
            this.db = db;
        }

        public ActionResult Menu()
        {
            //PREVENTING DEADLOCK
            // trick to prevent deadlocks of calling async method 
            // and waiting for on a sync UI thread.
            var syncContext = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(null);

            //  this is the async call, wait for the result (!)
            var results = db.GetAll().Result;
            IEnumerable <MenuViewModel> menuViewModels = results.Select(vm=>vm.ToMenuViewModel()).ToList();

            // restore the context
            SynchronizationContext.SetSynchronizationContext(syncContext);

            return PartialView("_Navigation", menuViewModels);
        }
    }
}