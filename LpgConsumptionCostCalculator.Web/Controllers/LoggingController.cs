using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class LoggingController : Controller
    {
        private readonly ILoginData _db;

        public LoggingController(ILoginData db)
        {
            _db = db;
        }

        [Authorize(Users = "Mateusz Donhefner")]
        public async Task<ActionResult> Logs()
        {
            //Get Okta user data
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var userEmail = claims.Where(x => x.Type == "email").FirstOrDefault().Value;
            var userOktaId = claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            var currentLoginTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");

            //Save data
            var currentUserLogin = new LoginData()
            {
                userId = userOktaId,
                timeStampUtc = currentLoginTime
            };
            await _db.AddUserData(currentUserLogin);

            //Retrieve data
            var dbLogins = await _db.GetUserData(currentUserLogin);

            var timeStampList = new List<string>();

            foreach (var login in dbLogins)
            {
                timeStampList.Add(login.timeStampUtc);
            }
            ViewBag.CurrentUser = currentUserLogin.userId;
            ViewBag.Logins = timeStampList.OrderByDescending(d => d);
            return View();
        }
    }
}