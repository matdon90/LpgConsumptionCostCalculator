using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Interfaces
{
    public interface IFuelReceiptData
    {
        Task<IEnumerable<FuelReceipt>> GetAll();
        Task<FuelReceipt> Get(int id);
        Task Add(FuelReceipt receipt);
        Task Update(FuelReceipt receipt);
        Task Delete(int id);
    }
}
