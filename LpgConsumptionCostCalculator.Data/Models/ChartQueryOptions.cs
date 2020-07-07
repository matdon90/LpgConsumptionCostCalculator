using System;

namespace LpgConsumptionCostCalculator.Data.Models
{
    public class ChartQueryOptions
    {
        public ChartQueryOptions()
        {
            startingTimeRange = DateTime.Now.AddMonths(-3);
        }
        public DateTime startingTimeRange { get; set; }
    }
}
