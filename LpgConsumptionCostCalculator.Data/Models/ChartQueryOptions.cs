using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Models
{
    public class ChartQueryOptions
    {
        public ChartQueryOptions()
        {
            sortFuelType = TypeOfFuel.LPG;
            startingTimeRange = DateTime.Now.AddMonths(-1);
        }
        public TypeOfFuel sortFuelType { get; set; }
        public DateTime startingTimeRange { get; set; }
    }
}
