using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseDatabaseLoginData : ILoginData
    {
        private FirebaseClient firebase;

        public FirebaseDatabaseLoginData()
        {
            this.firebase = new FirebaseConn().firebase;
        }

        public async Task AddUserData(LoginData user)
        {
            await firebase
                .Child("users/" + user.userId + "/logins/")
                .PostAsync<LoginData>(user);
        }
        public async Task<IEnumerable<LoginData>> GetUserData(LoginData user)
        {
            var result = await firebase
                .Child("users/")
                .Child(user.userId)
                .Child("logins/")
                .OnceAsync<LoginData>();
            return result.Select(u => new LoginData
            {
                userId = u.Object.userId,
                timeStampUtc = u.Object.timeStampUtc
            }).Where(c => c.userId == user.userId);
        }
    }

}
