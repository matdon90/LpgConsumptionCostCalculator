using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Web.ViewModels;

namespace LpgConsumptionCostCalculator.Web.Infrastructure
{
    /// <summary>
    /// Static extension
    /// </summary>
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }
        #region FuelReceipt
        public static FuelReceiptViewModel ToViewModel(this FuelReceipt entity)
        {
            return entity.MapTo<FuelReceipt, FuelReceiptViewModel>();
        }
        public static FuelReceipt ToEntityModel(this FuelReceiptViewModel model)
        {
            return model.MapTo<FuelReceiptViewModel, FuelReceipt>();
        }
        #endregion

        #region Menu
        public static MenuViewModel ToMenuViewModel(this Car entity)
        {
            return entity.MapTo<Car, MenuViewModel>();
        }
        #endregion

        #region Car
        public static CarViewModel ToViewModel(this Car entity)
        {
            return entity.MapTo<Car, CarViewModel>();
        }
        public static Car ToEntityModel(this CarViewModel model)
        {
            return model.MapTo<CarViewModel, Car>();
        }
        #endregion
    }
}