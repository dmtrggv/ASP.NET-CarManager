using CarManager.Models;

namespace CarManager.Services
{
    public interface IGarageManagementService
    {
        IEnumerable<GarageModel> GetAllGarages();

        GarageModel GetGarageById(int id);

        void AddGarage(GarageModel garage);

        void UpdateGarage(GarageModel garage, List<int> selectedCarIds);

        void DeleteGarage(int id);

        void UpdateCar(CarModel car);

        IEnumerable<CarModel> GetAllCars();
    }
}
