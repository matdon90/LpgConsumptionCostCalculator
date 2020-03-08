using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class ChartViewModel
    {
        public DateTime[] dateTimesArray { get; set; }
        public decimal[] consumptionArray { get; set; }
        public decimal[] priceArray { get; set; }
        public decimal averageFuelCons { get; set; }
        public decimal minFuelCons { get; set; }
        public decimal maxFuelCons { get; set; }
        public decimal avgPrice { get; set; }
        public decimal totalPrice { get; set; }
    }
}