using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using LpgConsumptionCostCalculator.Data.Interfaces;
using LpgConsumptionCostCalculator.Data.Models;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseDatabaseCarData : ICarData
    {
        private const String node = "cars/";
        private FirebaseClient firebase;

        public FirebaseDatabaseCarData()
        {
            this.firebase = new FirebaseConn().firebase;
        }

        public async Task Add(Car car)
        {
            IEnumerable<Car> cars = await GetAll();
            car.Id = cars.Count() != 0 ? cars.Max(c => c.Id) + 1 : 1;
            await firebase.Child(node).PostAsync<Car>(car);
        }

        public async Task Delete(int id)
        {
            var results = await firebase.Child(node).OnceAsync<Car>();
            var car = results.FirstOrDefault(o => o.Object.Id == id);
            if (car != null)
            {
                await firebase.Child(node).Child(car.Key).DeleteAsync();

                //logic for deleting receipts binded to deleted car
                var resultsWithReceipts = await firebase.Child("receipts/").OnceAsync<FuelReceipt>();
                foreach (var receiptObject in resultsWithReceipts)
                {
                    if (receiptObject.Object.FueledCarId == id)
                    {
                        await firebase.Child("receipts/").Child(receiptObject.Key).DeleteAsync();
                    }
                }
            }
        }

        public async Task<Car> Get(int id)
        {
            var results = await firebase.Child(node).OnceAsync<Car>();
            return results.Select(o => new Car
            {
                Id = o.Object.Id,
                CarModel = o.Object.CarModel,
                CarProducer = o.Object.CarProducer,
                CarProductionYear = o.Object.CarProductionYear,
                LpgInstallationModel = o.Object.LpgInstallationModel,
                LpgInstallationProducer = o.Object.LpgInstallationProducer
            }).FirstOrDefault(c => c.Id == id);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            var results = await firebase.Child(node).OnceAsync<Car>();
            var cars = results.Select(o => new Car
            {
                Id = o.Object.Id,
                CarModel = o.Object.CarModel,
                CarProducer = o.Object.CarProducer,
                CarProductionYear = o.Object.CarProductionYear,
                LpgInstallationModel = o.Object.LpgInstallationModel,
                LpgInstallationProducer = o.Object.LpgInstallationProducer
            }).ToList();
            return cars.OrderBy(c => c.CarProducer).ThenBy(c => c.CarModel).ThenBy(c => c.CarProductionYear);
        }

        public async Task Update(Car car)
        {
            var results = await firebase.Child(node).OnceAsync<Car>();
            var carDb = results.FirstOrDefault(o => o.Object.Id == car.Id);
            await firebase.Child(node).Child(carDb.Key).PutAsync<Car>(car);
        }
    }
}
