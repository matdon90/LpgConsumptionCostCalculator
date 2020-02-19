using LpgConsumptionCostCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
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
