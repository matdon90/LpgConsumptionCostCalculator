using LpgConsumptionCostCalculator.Web.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace LpgConsumptionCostCalculator.Web.ViewModels
{
    public class ReportConfigureViewModel
    {
        [Display(Name = "ReceiptCarId", ResourceType = typeof(RModels))]
        public int CarId { get; set; }
        public string CarData { get; set; }
        [Display(Name = "ReportStartDate", ResourceType = typeof(RModels))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "ReportEndDate", ResourceType = typeof(RModels))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}