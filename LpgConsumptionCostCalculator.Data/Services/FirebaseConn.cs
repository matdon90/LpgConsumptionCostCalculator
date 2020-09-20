using Firebase.Database;
using System;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseConn
    {
        private const string databaseUrl = "yourDatabaseUrl";
        private const string databaseSecret = "yourDatabaseSecret";
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
