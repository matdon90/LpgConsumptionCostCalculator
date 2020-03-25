using LpgConsumptionCostCalculator.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LpgConsumptionCostCalculator.Web.App_Start
{
    public class AutoMapperConfig
    {
        /// <summary>
        /// Configuration initialization of AutoMapper
        /// </summary>
        public static void AutoMapperRegister()
        {
            new AutoMapperStartupTask().Execute();
        }
    }
}