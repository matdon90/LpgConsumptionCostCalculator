using LpgConsumptionCostCalculator.Web.Content.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace LpgConsumptionCostCalculator.Web.Validations
{
    public class CheckCarProductionDate : ValidationAttribute
    {
        int currentYear = (int)DateTime.Now.Year;
        int firstCarProductionDate = 1885;

        protected override ValidationResult IsValid(object date, ValidationContext validationContext)
        {
            if ((int)date > currentYear)
            {
                return new ValidationResult(RError.CarProductionFuture + $" {currentYear}.");
            }
            if ((int)date < firstCarProductionDate)
            {
                return new ValidationResult(RError.CarProductionPast + $" { firstCarProductionDate}.");
            }
            return ValidationResult.Success;
        }
    }
}
