using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Models
{
    public class Car
    {
        public Car()
        {

        }
        public int CarId { get; set; }
        [Required]
        [Display(Name = "Producer")]
        public string CarProducer { get; set; }
        [Required]
        [Display(Name = "Model")]
        public string CarModel { get; set; }
        [Required]
        [Display(Name = "Production year")]
        public int CarProductionYear { get; set; }
        [Required]
        [Display(Name = "LPG installation producer")]
        public string LpgInstallationProducer { get; set; }
        [Required]
        [Display(Name = "LPG installation model")]
        public string LpgInstallationModel { get; set; }
    }

}
