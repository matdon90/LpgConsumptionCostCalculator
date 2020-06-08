using LpgConsumptionCostCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class FuelReceiptViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Car ID")]
        public int FueledCarId { get; set; }
        [Required(ErrorMessage ="Please enter date of refueling.")]
        [Display(Name = "Refueling date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime RefuelingDate { get; set; }
        [Display(Name = "Petrol station name")]
        public string PetrolStationName { get; set; }
        [Required(ErrorMessage = "Please enter fuel type.")]
        [Display(Name = "Fuel type")]
        public TypeOfFuel FuelType { get; set; }
        [Required(ErrorMessage = "Please enter fuel price.")]
        [Display(Name = "Fuel price")]
        public decimal FuelPrice { get; set; }
        [Required(ErrorMessage = "Please enter fuel amount.")]
        [Display(Name = "Fuel amount")]
        public decimal FuelAmount { get; set; }
        [Required(ErrorMessage = "Please enter distance from last refueling.")]
        [Display(Name = "Distance on the tank")]
        public decimal DistanceFromLastRefueling { get; set; }
        [MaxLength(255)]
        public string Comment { get; set; }
        [Display(Name = "Consumption/100 km")]
        public decimal FuelConsumption
        {
            get { return (FuelAmount / DistanceFromLastRefueling * 100); }
        }
        [Display(Name = "Total price/100 km")]
        public decimal PriceFor100km
        {
            get { return FuelPrice * (FuelAmount / DistanceFromLastRefueling * 100); }
        }


    }
}