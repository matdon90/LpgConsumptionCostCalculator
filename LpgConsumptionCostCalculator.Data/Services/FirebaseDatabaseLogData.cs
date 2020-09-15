using Firebase.Database;
using Firebase.Database.Query;
using LpgConsumptionCostCalculator.Data.Interfaces;
using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseDatabaseLogData : ILogData
    {
        private FirebaseClient firebase;

        public FirebaseDatabaseLogData()
        {
            this.firebase = new FirebaseConn().firebase;
        }

        public void AddUserData(LogData log)
        {
            firebase.Child("logs/").PostAsync<LogData>(log);
        }

        public async Task<IEnumerable<LogData>> GetUsersData()
        {
            var result = await firebase.Child("logs/").OnceAsync<LogData>();

            return result.Select(l => new LogData
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
