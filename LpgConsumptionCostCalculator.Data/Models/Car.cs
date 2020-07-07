using LpgConsumptionCostCalculator.Data.Validations;
using System.ComponentModel.DataAnnotations;

namespace LpgConsumptionCostCalculator.Data.Models
{
    public class Car
    {
        public Car()
        {

        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter car manufacturer.")]
        [Display(Name = "Manufacturer")]
        public string CarProducer { get; set; }
        [Required(ErrorMessage = "Please enter car model.")]
        [Display(Name = "Model")]
        public string CarModel { get; set; }
        [Required(ErrorMessage = "Please enter car production year.")]
        [CheckCarProductionDate]
        [Display(Name = "Production year")]
        public int CarProductionYear { get; set; }
        [Required(ErrorMessage = "Please enter LPG installation manufacturer.")]
        [Display(Name = "LPG installation manufacturer")]
        public string LpgInstallationProducer { get; set; }
        [Required(ErrorMessage = "Please enter LPG installation model.")]
        [Display(Name = "LPG installation model")]
        public string LpgInstallationModel { get; set; }
    }

}
