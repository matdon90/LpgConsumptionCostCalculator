using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using LpgConsumptionCostCalculator.Data.Interfaces;
using LpgConsumptionCostCalculator.Data.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<FirebaseDatabaseCarData>()
                .As<ICarData>()
                .InstancePerRequest();
            builder.RegisterType<FirebaseDatabaseLogData>()
               .As<ILogData>()
               .InstancePerRequest();
            builder.RegisterType<FirebaseDatabaseFuelReceiptData>()
                .As<IFuelReceiptData>()
                .InstancePerRequest();
            builder.RegisterType<FirebaseDatabaseCarData>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}