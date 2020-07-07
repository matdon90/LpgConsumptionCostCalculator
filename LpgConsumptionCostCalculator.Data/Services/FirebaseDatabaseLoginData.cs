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

        public void AddUserData(LoginData log)
        {
            firebase.Child("logs/").PostAsync<LoginData>(log);
        }

        public async Task<IEnumerable<LoginData>> GetUsersData()
        {
            var result = await firebase.Child("logs/").OnceAsync<LoginData>();

            return result.Select(l => new LoginData
            {
                Id = l.Object.Id,
                UserName = l.Object.UserName,
                LogTime = l.Object.LogTime,
                RequestDuration = l.Object.RequestDuration,
                LogMessage = l.Object.LogMessage
            }).ToList().OrderByDescending(l => l.LogTime);
        }
    }

}
