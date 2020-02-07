using LpgConsumptionCostCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class CarViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
    }
}