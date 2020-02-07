using LpgConsumptionCostCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public class InMemoryCarData : ICarData
    {
        List<Car> cars;

        public InMemoryCarData()
        {
            cars = new List<Car>()
            {
                new Car()
                {
                    CarId = 1,
                    CarProducer = "KIA",
                    CarModel = "Ceed Kombi 1.4 MPI",
                    CarProductionYear = 2019,
                    LpgInstallationProducer = "BRC",
                    LpgInstallationModel = "Sequent32 ODB"
                },
                new Car()
                {
                    CarId = 2,
                    CarProducer = "Skoda",
                    CarModel = "Fabia 1.2 TSI",
                    CarProductionYear = 2015,
                    LpgInstallationProducer = "LandiRenzo",
                    LpgInstallationModel = "LandiRenzo"
                }
            };
        }
        /// <summary>
        /// Adds new cart in program memory.
        /// </summary>
        /// <param name="car"></param>
        public void Add(Car car)
        {
            car.CarId = cars.Max(c => c.CarId) + 1;
            cars.Add(car);
        }

        /// <summary>
        /// Deletes car with chosen ID from in memory car list.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var car = Get(id);
            if (car != null)
            {
                cars.Remove(car);
            }
        }

        /// <summary>
        /// Gets a car by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Car Get(int id)
        {
            return cars.FirstOrDefault(c => c.CarId == id);
        }

        /// <summary>
        /// Gets all cars from in memory list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetAll()
        {
            return cars.OrderBy(c => c.CarProducer).ThenBy(c => c.CarModel).ThenBy(c => c.CarProductionYear);
        }

        /// <summary>
        /// Udpates a car with selected ID.
        /// </summary>
        /// <param name="car"></param>
        public void Update(Car car)
        {
            var existingCar = Get(car.CarId);
            if (existingCar != null)
            {
                existingCar.CarModel = car.CarModel;
                existingCar.CarProducer = car.CarProducer;
                existingCar.CarProductionYear = car.CarProductionYear;
                existingCar.LpgInstallationModel = car.LpgInstallationModel;
                existingCar.LpgInstallationProducer = car.LpgInstallationProducer;
            }
        }
    }
}
