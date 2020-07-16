using LpgConsumptionCostCalculator.Web.Infrastructure;

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