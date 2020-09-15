using LpgConsumptionCostCalculator.Data.Enums;
using System;

namespace LpgConsumptionCostCalculator.Data.Models
{
    public class FuelReceipt
    {
        public FuelReceipt()
        {

        }
        public FuelReceipt(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public int FueledCarId { get; set; }
        public DateTime RefuelingDate { get; set; }
        public string PetrolStationName { get; set; }
        public TypeOfFuel FuelType { get; set; }
        public decimal FuelPrice { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal DistanceFromLastRefueling { get; set; }
        public string Comment { get; set; }

    }
}
