using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Services
{
    public interface ICarData
    {
        IEnumerable<Car> GetAll();
        Car Get(int id);
        Task Add(Car car);
        void Update(Car car);
        void Delete(int id);
    }
}
