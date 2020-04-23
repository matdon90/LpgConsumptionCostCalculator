using LpgConsumptionCostCalculator.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly ICarData dbCar;
        private readonly IFuelReceiptData dbReceipt;

        public ReportController(ICarData dbCar, IFuelReceiptData dbReceipt)
        {
            this.dbCar = dbCar;
            this.dbReceipt = dbReceipt;
        }

        // GET: Raport
        [HttpGet]
        public async Task<ActionResult> Configure(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var modelCar = await dbCar.Get(id);
                var modelReceipt = await dbReceipt.GetAll();
                if (modelCar == null)
                {
                    Response.StatusCode = 403;
                    return View("NotFound");
                }
                return PartialView(modelCar);
            }
            else
            {
                Response.StatusCode = 500;
                return View("NotFound");
            }
        }
    }
}