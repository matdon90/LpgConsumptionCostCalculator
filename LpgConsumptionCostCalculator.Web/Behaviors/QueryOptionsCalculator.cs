using LpgConsumptionCostCalculator.Data.QueryOptions;
using System;

namespace LpgConsumptionCostCalculator.Web.Behaviors
{
    public class QueryOptionsCalculator
    {
        public static int CalculateStartPage(TableQueryOptions queryOptions)
        {
            return (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
        }
        public static int CalculateTotalPages(int count, int pageSize)
        {
            return (int)Math.Ceiling((double)count / pageSize);
        }
    }
}