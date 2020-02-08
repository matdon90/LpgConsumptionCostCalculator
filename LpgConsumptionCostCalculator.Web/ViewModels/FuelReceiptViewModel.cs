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
        public int FuelReceiptId { get; set; }
        public int FueledCarId { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime RefuelingDate { get; set; }
        [Display(Name = "Petrol station")]
        public string PetrolStationName { get; set; }
        [Required]
        [Display(Name = "Type of fuel")]
        public TypeOfFuel FuelType { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal FuelPrice { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public decimal FuelAmount { get; set; }
        [Required]
        [Display(Name = "Distance on this tank")]
        public decimal DistanceFromLastRefueling { get; set; }
        [MaxLength(255)]
        public string Comment { get; set; }
        [Display(Name = "Consumption/100 km")]
        public decimal FuelConsumption
        {
            get { return (FuelAmount / DistanceFromLastRefueling * 100); }
        }
        [Display(Name = "Price/100 km")]
        public decimal PriceFor100km
        {
            get { return FuelPrice * (FuelAmount / DistanceFromLastRefueling * 100); }
        }


    }
}