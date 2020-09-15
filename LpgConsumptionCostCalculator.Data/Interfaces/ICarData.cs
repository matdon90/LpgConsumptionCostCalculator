using LpgConsumptionCostCalculator.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LpgConsumptionCostCalculator.Data.Interfaces
{
    public interface ICarData
    {
        Task<IEnumerable<Car>> GetAll();
        Task<Car> Get(int id);
        Task Add(Car car);
        Task Update(Car car);
        Task Delete(int id);
    }
}
