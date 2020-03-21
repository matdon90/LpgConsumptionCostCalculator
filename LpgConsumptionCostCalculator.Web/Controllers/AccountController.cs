using Microsoft.Owin.Security.Cookies;
using Okta.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginData db;
        public AccountController(ILoginData db)
        {
            this.db = db;
        }
        public ActionResult Login()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    OktaDefaults.MvcAuthenticationType);
                return new HttpUnauthorizedResult();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(
                    CookieAuthenticationDefaults.AuthenticationType,
                    OktaDefaults.MvcAuthenticationType);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PostLogout()
        {
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
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
            await db.AddUserData(currentUserLogin);

            //Retrieve data
            var dbLogins = await db.GetUserData(currentUserLogin);

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