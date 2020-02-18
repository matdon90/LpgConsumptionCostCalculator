using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LpgConsumptionCostCalculator.Data.Models;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class InMemoryFuelReceiptData : IFuelReceiptData
    {
        List<FuelReceipt> fuelReceipts;

        public InMemoryFuelReceiptData()
        {
            fuelReceipts = new List<FuelReceipt>()
            {
                new FuelReceipt()
                {
                    FuelReceiptId = 1,
                    RefuelingDate = new DateTime(DateTime.Now.Year, 2, 1),
                    PetrolStationName = "Shell",
                    FuelType = TypeOfFuel.LPG,
                    FuelPrice = 2.34M,
                    FuelAmount = 44.05M,
                    DistanceFromLastRefueling = 481,
                    Comment = "Testowy paragon 1",
                    FueledCarId = 1
                },
                new FuelReceipt()
                {
                    FuelReceiptId = 2,
                    RefuelingDate = new DateTime(DateTime.Now.Year, 1, 13),
                    PetrolStationName = "BP",
                    FuelType = TypeOfFuel.LPG,
                    FuelPrice = 2.49M,
                    FuelAmount = 42.66M,
                    DistanceFromLastRefueling = 450,
                    Comment = "Testowy paragon 2",
                    FueledCarId = 2
                },
                new FuelReceipt()
                {
                    FuelReceiptId = 3,
                    RefuelingDate = new DateTime(DateTime.Now.Year, 2, 6),
                    PetrolStationName = "Orlen",
                    FuelType = TypeOfFuel.LPG,
                    FuelPrice = 1.99M,
                    FuelAmount = 46.87M,
                    DistanceFromLastRefueling = 502,
                    Comment = "Testowy paragon 3",
                    FueledCarId = 1
                }
            };
        }
        public async Task Add(FuelReceipt receipt)
        {
            receipt.FuelReceiptId = fuelReceipts.Max(r => r.FuelReceiptId) + 1;
            fuelReceipts.Add(receipt);
        }

        public async Task Delete(int id)
        {
            var receipt = await Get(id);
            if (receipt != null)
            {
                fuelReceipts.Remove(receipt);
            }
        }

        public async Task<FuelReceipt> Get(int id)
        {
            return fuelReceipts.FirstOrDefault(r => r.FuelReceiptId == id);
        }

        public async Task<IEnumerable<FuelReceipt>> GetAll()
        {
            return fuelReceipts.OrderBy(r => r.RefuelingDate).ThenBy(r => r.PetrolStationName);
        }

        public async Task Update(FuelReceipt receipt)
        {
            var existingReceipt = await Get(receipt.FuelReceiptId);
            if (existingReceipt != null)
            {
                existingReceipt.FuelReceiptId = receipt.FuelReceiptId;
                existingReceipt.RefuelingDate = receipt.RefuelingDate;
                existingReceipt.PetrolStationName = receipt.PetrolStationName;
                existingReceipt.FuelType = receipt.FuelType;
                existingReceipt.FuelPrice = receipt.FuelPrice;
                existingReceipt.FuelAmount = receipt.FuelAmount;
                existingReceipt.DistanceFromLastRefueling = receipt.DistanceFromLastRefueling;
                existingReceipt.Comment = receipt.Comment;
                existingReceipt.FueledCarId = receipt.FueledCarId;
            }
        }
    }
}
