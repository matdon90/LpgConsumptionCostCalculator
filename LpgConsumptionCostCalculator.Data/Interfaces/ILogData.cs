using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Interfaces
{
    public interface ILogData
    {
        Task<IEnumerable<LogData>> GetUsersData();
        void AddUserData(LogData user);
    }
}
