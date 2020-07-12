namespace LpgConsumptionCostCalculator.Data.Models
{
    public class Car
    {
        public Car()
        {

        }
        public int Id { get; set; }
        public string CarProducer { get; set; }
        public string CarModel { get; set; }
        public int CarProductionYear { get; set; }
        public string LpgInstallationProducer { get; set; }
        public string LpgInstallationModel { get; set; }
    }

}
