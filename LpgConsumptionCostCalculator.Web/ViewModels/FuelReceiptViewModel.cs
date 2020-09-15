using LpgConsumptionCostCalculator.Data.Enums;
using LpgConsumptionCostCalculator.Web.Content.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class FuelReceiptViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "ReceiptCarId", ResourceType = typeof(RModels))]
        public int FueledCarId { get; set; }
        [Required(ErrorMessageResourceName = "ReceiptDate", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "ReceiptRefuellingDate", ResourceType = typeof(RModels))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime RefuelingDate { get; set; }
        [Display(Name = "ReceiptPetrolStationName", ResourceType = typeof(RModels))]
        public string PetrolStationName { get; set; }
        [Required(ErrorMessageResourceName = "ReceiptType", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "ReceiptFuelType", ResourceType = typeof(RModels))]
        public TypeOfFuel FuelType { get; set; }
        [Required(ErrorMessageResourceName = "ReceiptPrice", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "ReceiptFuelPrice", ResourceType = typeof(RModels))]
        public decimal FuelPrice { get; set; }
        [Required(ErrorMessageResourceName = "ReceiptAmount", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "ReceiptFuelAmount", ResourceType = typeof(RModels))]
        public decimal FuelAmount { get; set; }
        [Required(ErrorMessageResourceName = "ReceiptDistance", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "ReceiptDistanceOnTank", ResourceType = typeof(RModels))]
        public decimal DistanceFromLastRefueling { get; set; }
        [Display(Name = "ReceiptComment", ResourceType = typeof(RModels))]
        [MaxLength(255,ErrorMessageResourceName = "ReceiptComment", ErrorMessageResourceType = typeof(RError))]
        public string Comment { get; set; }
        [Display(Name = "ReceiptConsumption", ResourceType = typeof(RModels))]
        public decimal FuelConsumption
        {
            get { return (FuelAmount / DistanceFromLastRefueling * 100); }
        }
        [Display(Name = "ReceiptTotalPrice", ResourceType = typeof(RModels))]
        public decimal PriceFor100km
        {
            get { return FuelPrice * (FuelAmount / DistanceFromLastRefueling * 100); }
        }


    }
}