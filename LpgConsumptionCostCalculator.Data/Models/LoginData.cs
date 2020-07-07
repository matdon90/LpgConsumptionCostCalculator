using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Models
{
    public class LoginData
    {
        public string userName { get; set; }
        public DateTime logTime { get; set; }
        public string requestDuration { get; set; }
        public string logMessage { get; set; }
    }
}
