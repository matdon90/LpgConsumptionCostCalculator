using LpgConsumptionCostCalculator.Data.Enums;

namespace LpgConsumptionCostCalculator.Data.QueryOptions
{
    public class TableQueryOptions
    {
        public TableQueryOptions()
        {
            CurrentPage = 1;
            PageSize = 10;
            SortField = "Id";
            SortOrder = SortOrder.descending;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public SortOrder SortOrder { get; set; }
        public string Sort
        {
            get
            {
                return string.Format("{0} {1}", SortField, SortOrder.ToString());
            }
        }
    }
}
