using LpgConsumptionCostCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class ExportPdfViewModel
    {
        public string ReportAuthor { get; set; }
        public string CarData { get; set; }
        public string RefuelingDate { get; set; }
        public string PetrolStationName { get; set; }
        public string FuelType { get; set; }
        public string FuelPrice { get; set; }
        public string FuelAmount { get; set; }
        public string DistanceFromLastRefueling { get; set; }
        public string FuelConsumption { get; set; }
        public string PriceFor100km { get; set; }
        public string AverageFuelCons { get; set; }
        public string MinFuelCons { get; set; }
        public string MaxFuelCons { get; set; }
        public string AvgPrice { get; set; }
        public string TotalPrice { get; set; }
        public string TotalDistance { get; set; }
        public string[] LpgDateTimesArray { get; set; }
        public decimal[] LpgConsumptionArray { get; set; }
    }
}