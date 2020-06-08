using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Validations
{
    public class CheckCarProductionDate : ValidationAttribute
    {
        int currentYear = (int)DateTime.Now.Year;
        int firstCarProductionDate = 1885;

        protected override ValidationResult IsValid(object date, ValidationContext validationContext)
        {
            if ((int)date > currentYear)
            {
                return new ValidationResult($"Production date cannot be from future, bigger than {currentYear}.");
            }
            if ((int)date < firstCarProductionDate)
            {
                return new ValidationResult($"Production date cannot be smaller than {firstCarProductionDate}.");
            }
            return ValidationResult.Success;
        }
    }
}
