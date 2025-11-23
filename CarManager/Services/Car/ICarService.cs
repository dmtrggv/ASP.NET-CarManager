using CarManager.Models;
using System.Collections.Generic;

namespace CarManager.Services
{
    public interface ICarService
    {
        IEnumerable<CarModel> GetAllCars();
        CarModel GetCarById(int id);
        void AddCar(CarModel car);
        void UpdateCar(CarModel car);
        void DeleteCar(int id);
        IEnumerable<EngineModel> GetAllEngines();
    }
}
