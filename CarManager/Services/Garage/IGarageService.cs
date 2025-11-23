using CarManager.Models;

namespace CarManager.Services.Garage
{
    public interface IGarageService
    {
        IEnumerable<CarModel> GetAllCars();
        CarModel GetCarById(int id);
        void AddCar(CarModel car);
        void UpdateCar(CarModel car);
        void DeleteCar(int id);
    }
}
