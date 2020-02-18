using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using LpgConsumptionCostCalculator.Data.Models;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseDatabaseFuelReceiptData :IFuelReceiptData
    {
        private const String databaseUrl = "https://lpgconsumptioncostcalculator.firebaseio.com/";
        private const String databaseSecret = "EzKZld9GBLP5o6BzvWQqXqpQeD1NHIDQ7NdPKKu2";
        private const String node = "receipts/";

        private FirebaseClient firebase;

        public FirebaseDatabaseFuelReceiptData()
        {
            this.firebase = new FirebaseClient(
                databaseUrl
                //,
                //new FirebaseOptions
                //{
                //    AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                //}
                );
        }

        public async Task Add(FuelReceipt receipt)
        {
            var receipts = await GetAll();

            if (receipts.Count() != 0)
            {
                receipt.FuelReceiptId = receipts.Max(r => r.FuelReceiptId) + 1;
            }
            else
            {
                receipt.FuelReceiptId = 1;
            }
            await firebase.Child(node).PostAsync<FuelReceipt>(receipt);
        }

        public async Task Delete(int id)
        {
            var results = await firebase.Child(node).OnceAsync<FuelReceipt>();
            var receipt = results.FirstOrDefault(o => o.Object.FuelReceiptId == id);
            if (receipt != null)
            {
                await firebase.Child(node).Child(receipt.Key).DeleteAsync();
            }
        }

        public async Task<FuelReceipt> Get(int id)
        {
            var results = await firebase.Child(node).OnceAsync<FuelReceipt>();
            return results.Select(r => new FuelReceipt
            {
                FuelReceiptId = r.Object.FuelReceiptId,
                FuelAmount = r.Object.FuelAmount,
                Comment = r.Object.Comment,
                DistanceFromLastRefueling = r.Object.DistanceFromLastRefueling,
                FueledCarId = r.Object.FueledCarId,
                FuelPrice = r.Object.FuelPrice,
                FuelType = r.Object.FuelType,
                PetrolStationName = r.Object.PetrolStationName,
                RefuelingDate = r.Object.RefuelingDate
            }).FirstOrDefault(r => r.FuelReceiptId == id);
        }

        public async Task<IEnumerable<FuelReceipt>> GetAll()
        {
            var results = await firebase.Child(node).OnceAsync<FuelReceipt>();
            var receipts = results.Select(r => new FuelReceipt
            {
                FuelReceiptId = r.Object.FuelReceiptId,
                FuelAmount = r.Object.FuelAmount,
                Comment = r.Object.Comment,
                DistanceFromLastRefueling = r.Object.DistanceFromLastRefueling,
                FueledCarId = r.Object.FueledCarId,
                FuelPrice = r.Object.FuelPrice,
                FuelType = r.Object.FuelType,
                PetrolStationName = r.Object.PetrolStationName,
                RefuelingDate = r.Object.RefuelingDate
            }).ToList();
            return receipts.OrderBy(r => r.RefuelingDate).ThenBy(r => r.PetrolStationName);

        }

        public async Task Update(FuelReceipt receipt)
        {
            var results = await firebase.Child(node).OnceAsync<FuelReceipt>();
            var receiptDb = results.FirstOrDefault(r => r.Object.FuelReceiptId == receipt.FuelReceiptId);
            await firebase.Child(node).Child(receiptDb.Key).PutAsync<FuelReceipt>(receipt);
        }
    }
}
