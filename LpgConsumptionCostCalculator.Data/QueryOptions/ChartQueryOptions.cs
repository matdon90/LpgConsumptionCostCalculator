using System;

namespace LpgConsumptionCostCalculator.Data.QueryOptions
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
