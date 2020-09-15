using LpgConsumptionCostCalculator.Data.Models;
using LpgConsumptionCostCalculator.Web.Infrastructure;
using LpgConsumptionCostCalculator.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LpgConsumptionCostCalculator.Web.Behaviors
{
    public static class CsvGenerator
    {
        public static byte[] CreateCsvBody(ReportConfigureViewModel reportConfigViewModel, IEnumerable<FuelReceipt> receiptsModel)
        {
            //transfer receipts list for CSV
            List<object> receipts = receiptsModel.Where(m => m.FueledCarId == reportConfigViewModel.CarId)
                        .Where(m => m.RefuelingDate >= reportConfigViewModel.StartDate)
                        .Where(m => m.RefuelingDate <= reportConfigViewModel.EndDate)
                        .Select(vm => vm.ToViewModel())
                        .Select(vm => new[]
                            {
                                            vm.RefuelingDate.ToString("dd.MM.yyyy"),
                                            vm.PetrolStationName,
                                            vm.FuelType.ToString(),
                                            decimal.Round(vm.FuelPrice, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                                            decimal.Round(vm.FuelAmount, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                                            decimal.Round(vm.DistanceFromLastRefueling, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                                            decimal.Round(vm.FuelConsumption, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.'),
                                            decimal.Round(vm.PriceFor100km, 2, MidpointRounding.AwayFromZero).ToString().Replace(',','.')
                            }).ToList<object>();

            //add column names
            receipts.Insert(0, new string[8] { "Refueling date", "Petrol station", "Fuel type", "Fuel price", "Fuel amount", "Distance", "Consumption/100 km", "Total price/100 km" });

            //prepare string for csv
            var stringForCsv = BuildStringForCsv(receipts);
            return Encoding.UTF8.GetBytes(stringForCsv);
        }

        private static string BuildStringForCsv(List<object> receipts)
        {
            StringBuilder sbuild = new StringBuilder();
            for (int i = 0; i < receipts.Count; i++)
            {
                string[] receipt = (string[])receipts[i];
                for (int j = 0; j < receipt.Length; j++)
                {
                    if (j == receipt.Length - 1)
                    {
                        sbuild.Append(receipt[j]);
                    }
                    else
                    {
                        sbuild.Append(receipt[j] + ',');
                    }
                }
                sbuild.Append("\r\n");
            }
            return sbuild.ToString();
        }
    }
}