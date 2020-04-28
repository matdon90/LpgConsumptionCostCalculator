using System;
using System.ComponentModel.DataAnnotations;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class ReportConfigureViewModel
    {
        [Display(Name = "Car ID")]
        public int CarId { get; set; }
        public string CarData { get; set; }
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}