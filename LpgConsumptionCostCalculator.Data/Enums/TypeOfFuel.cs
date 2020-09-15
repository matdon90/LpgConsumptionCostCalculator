using LpgConsumptionCostCalculator.Data.Resources;
using System.ComponentModel.DataAnnotations;

namespace LpgConsumptionCostCalculator.Data.Enums
{
    public enum TypeOfFuel
    {
        [Display(ResourceType = typeof(RTypeOfFuel), Name = "None")]
        None,
        [Display(ResourceType = typeof(RTypeOfFuel), Name = "Petrol")]
        Petrol,
        [Display(ResourceType = typeof(RTypeOfFuel), Name = "LPG")]
        LPG,
        [Display(ResourceType = typeof(RTypeOfFuel), Name = "Diesel")]
        Diesel
    }
}
