using LpgConsumptionCostCalculator.Web.Resources;
using LpgConsumptionCostCalculator.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "CarManufacturerRequired", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "CarManufacturer", ResourceType = typeof(RModels))]
        public string CarProducer { get; set; }
        [Required(ErrorMessageResourceName = "CarModelRequired", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "CarModel", ResourceType = typeof(RModels))]
        public string CarModel { get; set; }
        [CheckCarProductionDate]
        [Required(ErrorMessageResourceName = "CarYearRequired", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "CarProductionYear", ResourceType = typeof(RModels))]
        public int CarProductionYear { get; set; }
        [Required(ErrorMessageResourceName = "CarLpgManufacturerRequired", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "CarLpgInstallationManufacturer", ResourceType = typeof(RModels))]
        public string LpgInstallationProducer { get; set; }
        [Required(ErrorMessageResourceName = "CarLpgModelRequired", ErrorMessageResourceType = typeof(RError))]
        [Display(Name = "CarLpgInstallationModel", ResourceType = typeof(RModels))]
        public string LpgInstallationModel { get; set; }
    }
}