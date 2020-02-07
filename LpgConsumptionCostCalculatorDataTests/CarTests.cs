using System;
using LpgConsumptionCostCalculator.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LpgConsumptionCostCalculatorDataTests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void ValidTest()
        {
            //Arrange
            string expectedCarProducer = "KIA";
            string expectedCarModel = "Ceed Kombi 1.4 MPI";
            int expectedCarProductionYear = 2019;
            string expectedLpgInstallationProducer = "BRC";
            string expectedLpgInstallationModel = "Sequent32 ODB";

            //Act
            Car car = new Car()
            {
                CarId=1,
                CarProducer = "KIA",
                CarModel = "Ceed Kombi 1.4 MPI",
                CarProductionYear = 2019,
                LpgInstallationProducer = "BRC",
                LpgInstallationModel = "Sequent32 ODB"
            };

            //Assert
            Assert.AreEqual(1, car.CarId);
            Assert.AreEqual(expectedCarProducer, car.CarProducer);
            Assert.AreEqual(expectedCarModel, car.CarModel);
            Assert.AreEqual(expectedCarProductionYear, car.CarProductionYear);
            Assert.AreEqual(expectedLpgInstallationProducer, car.LpgInstallationProducer);
            Assert.AreEqual(expectedLpgInstallationModel, car.LpgInstallationModel);
        }
    }
}
