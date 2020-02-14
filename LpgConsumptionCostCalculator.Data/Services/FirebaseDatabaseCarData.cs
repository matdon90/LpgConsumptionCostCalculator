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
        private const String databaseUrl = "https://lpgconsumptioncostcalculator.firebaseio.com/";
        private const String databaseSecret = "EzKZld9GBLP5o6BzvWQqXqpQeD1NHIDQ7NdPKKu2";
        private const String node = "cars/";

        private FirebaseClient firebase;

        public FirebaseDatabaseCarData()
        {
            this.firebase = new FirebaseClient(
                databaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                });
        }

        public async Task Add(Car car)
        {
            await firebase.Child(node).PostAsync<Car>(car);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Car Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
