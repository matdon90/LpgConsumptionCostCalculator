using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public interface ILoginData
    {
        Task<IEnumerable<LoginData>> GetUserData(LoginData user);
        Task AddUserData(LoginData user);
    }
}
