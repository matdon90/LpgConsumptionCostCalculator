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
        IEnumerable<FuelReceipt> GetAll();
        FuelReceipt Get(int id);
        void Add(FuelReceipt receipt);
        void Update(FuelReceipt receipt);
        void Delete(int id);
    }
}
