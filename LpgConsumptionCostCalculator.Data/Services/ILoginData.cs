using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public interface ILoginData
    {
        Task<IEnumerable<LoginData>> GetUsersData();
        void AddUserData(LoginData user);
    }
}
