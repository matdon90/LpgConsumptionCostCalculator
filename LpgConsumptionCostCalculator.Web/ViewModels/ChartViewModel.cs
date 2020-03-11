using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class ChartViewModel
    {
        public string[] lpgDateTimesArray { get; set; }
        public decimal[] lpgConsumptionArray { get; set; }
        public decimal[] lpgPriceArray { get; set; }
        public string[] petrolDateTimesArray { get; set; }
        public decimal[] petrolConsumptionArray { get; set; }
        public decimal[] petrolPriceArray { get; set; }
        public string[] dieselDateTimesArray { get; set; }
        public decimal[] dieselConsumptionArray { get; set; }
        public decimal[] dieselPriceArray { get; set; }
        public decimal averageFuelCons { get; set; }
        public decimal minFuelCons { get; set; }
        public decimal maxFuelCons { get; set; }
        public decimal avgPrice { get; set; }
        public decimal totalPrice { get; set; }
        public decimal totalDistance { get; set; }
    }
}