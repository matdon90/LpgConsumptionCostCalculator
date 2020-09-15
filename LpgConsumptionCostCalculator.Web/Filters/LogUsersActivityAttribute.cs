using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Data.Services;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LpgConsumptionCostCalculator.Web.Filters
{
    public class LogUsersActivityAttribute : ActionFilterAttribute
    {
        private FirebaseDatabaseLogData _db;

        public LogUsersActivityAttribute()
        {
            _db = new FirebaseDatabaseLogData();
        }

        private Stopwatch sw = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            sw.Reset();
            sw.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            sw.Stop();
            float actionDurationTime = sw.ElapsedMilliseconds;
            Log("OnActionExecuted", filterContext.RouteData, actionDurationTime);
        }

        private void Log(string methodName, RouteData routeData, float actionDurationTime)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var user = HttpContext.Current.GetOwinContext().Authentication.User.Identity.Name;
            var logTime = DateTime.Now;
            var requestDuration = actionDurationTime.ToString();
            var message = String.Format("LpgCCC Log - Method: {0} - Controller: {1} - Action: {2}", methodName, controllerName, actionName);
            var log = new LogData()
            {
                UserName = user,
                LogTime = logTime,
                RequestDuration = requestDuration,
                LogMessage = message
            };

            _db.AddUserData(log);
        }
    }
}