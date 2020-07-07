using Firebase.Database;
using Firebase.Database.Query;
using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseDatabaseLoginData : ILoginData
    {
        private FirebaseClient firebase;

        public FirebaseDatabaseLoginData()
        {
            this.firebase = new FirebaseConn().firebase;
        }

        public void AddUserData(LoginData user)
        {
            firebase.Child("logs/").PostAsync<LoginData>(user);
        }

        public async Task<IEnumerable<LoginData>> GetUsersData()
        {
            var result = await firebase.Child("logs/").OnceAsync<LoginData>();

            return result.Select(u => new LoginData
            {
                userName = u.Object.userName,
                logTime = u.Object.logTime,
                requestDuration = u.Object.requestDuration,
                logMessage = u.Object.logMessage
            }).ToList().OrderByDescending(l => l.logTime);
        }
    }

}
