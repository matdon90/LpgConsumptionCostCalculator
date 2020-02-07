using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public interface ICarData
    {
        IEnumerable<Car> GetAll();
        Car Get(int id);
        void Add(Car car);
        void Update(Car car);
        void Delete(int id);
    }
}
