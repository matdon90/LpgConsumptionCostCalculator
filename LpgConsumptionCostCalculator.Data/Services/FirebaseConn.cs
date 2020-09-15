using Firebase.Database;
using System;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseConn
    {
        private const string databaseUrl = "https://lpgconsumptioncostcalculator.firebaseio.com/";
        private const string databaseSecret = "EzKZld9GBLP5o6BzvWQqXqpQeD1NHIDQ7NdPKKu2";
        public FirebaseClient firebase;
        public FirebaseConn()
        {
            this.firebase = new FirebaseClient(
                databaseUrl,
                new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                    }
                );
        }
    }
}
