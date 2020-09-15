using System;
using System.Collections.Generic;
using LpgConsumptionCostCalculator.Data.Enums;
using LpgConsumptionCostCalculator.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LpgConsumptionCostCalculatorDataTests
{
    [TestClass]
    public class FuelReceiptTests
    {
        [TestMethod]
        public void ValidTest()
        {
            //Arrange

            //Act
            FuelReceipt receipt = new FuelReceipt()
            {
                FueledCarId = 1,
                RefuelingDate = new DateTime(DateTime.Now.Year, 2, 1),
                PetrolStationName = "Shell",
                FuelType = TypeOfFuel.LPG,
                FuelPrice = 2.34M,
                FuelAmount = 44.05M,
                DistanceFromLastRefueling = 481,
                Comment = "Testowy paragon"
            };

            //Assert
            Assert.AreEqual(1, receipt.Id);
            Assert.AreEqual(1, receipt.FueledCarId);
            Assert.AreEqual(new DateTime(DateTime.Now.Year, 2, 1), receipt.RefuelingDate);
            Assert.AreEqual("Shell", receipt.PetrolStationName);
            Assert.AreEqual(TypeOfFuel.LPG, receipt.FuelType);
            Assert.AreEqual(2.34M, receipt.FuelPrice);
            Assert.AreEqual(44.05M, receipt.FuelAmount);
            Assert.AreEqual(481, receipt.DistanceFromLastRefueling);
            Assert.AreEqual("Testowy paragon", receipt.Comment);
        }
    }
}
