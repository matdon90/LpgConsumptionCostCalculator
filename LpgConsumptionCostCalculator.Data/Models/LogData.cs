using System;

namespace LpgConsumptionCostCalculator.Data.Models
{
    public class LogData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime LogTime { get; set; }
        public string RequestDuration { get; set; }
        public string LogMessage { get; set; }
    }
}
