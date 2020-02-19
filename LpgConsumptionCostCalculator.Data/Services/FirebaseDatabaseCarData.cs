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
            car.CarId = cars.Count() != 0 ? cars.Max(c => c.CarId) + 1 : 1;
            await firebase.Child(node).PostAsync<Car>(car);
        }

        public async Task Delete(int id)
        {
            var results = await firebase.Child(node).OnceAsync<Car>();
            var car = results.FirstOrDefault(o => o.Object.CarId == id);
            if (car != null)
            {
                await firebase.Child(node).Child(car.Key).DeleteAsync();
            }
        }

        public async Task<Car> Get(int id)
        {
            var results = await firebase.Child(node).OnceAsync<Car>();
            return results.Select(o => new Car
            {
                CarId = o.Object.CarId,
                CarModel = o.Object.CarModel,
                CarProducer = o.Object.CarProducer,
                CarProductionYear = o.Object.CarProductionYear,
                LpgInstallationModel = o.Object.LpgInstallationModel,
                LpgInstallationProducer = o.Object.LpgInstallationProducer
            }).FirstOrDefault(c => c.CarId == id);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            var results = await firebase.Child(node).OnceAsync<Car>();
            var cars = results.Select(o => new Car
            {
                CarId = o.Object.CarId,
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
            var carDb = results.FirstOrDefault(o => o.Object.CarId == car.CarId);
            await firebase.Child(node).Child(carDb.Key).PutAsync<Car>(car);
        }
    }
}
