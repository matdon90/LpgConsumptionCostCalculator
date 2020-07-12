using AutoMapper;
using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Web.ViewModels;

namespace LpgConsumptionCostCalculator.Web.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                #region FuelReceipt
                cfg.CreateMap<FuelReceipt,FuelReceiptViewModel>();
                cfg.CreateMap<FuelReceiptViewModel, FuelReceipt>();
                #endregion

                #region Car
                cfg.CreateMap<Car, CarViewModel>();
                cfg.CreateMap<CarViewModel, Car>();
                #endregion

                #region Menu
                cfg.CreateMap<Car, MenuViewModel>()
                .ForMember(dest=>dest.CarId, opt=>opt.MapFrom(src =>src.Id));
                #endregion
            });

            Mapper = MapperConfiguration.CreateMapper();
        }
        public static IMapper Mapper { get; set; }
        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}