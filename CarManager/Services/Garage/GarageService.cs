using CarManager.Data;
using CarManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Services
{
    public class GarageService : IGarageManagementService
    {
        private readonly ApplicationDbContext _context;

        public GarageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GarageModel> GetAllGarages()
        {
            return _context.Garages.Include(g => g.Cars).AsNoTracking().ToList();
        }

        public GarageModel GetGarageById(int id)
        {
            return _context.Garages.Include(g => g.Cars).FirstOrDefault(g => g.Id == id);
        }

        public void UpdateCar(CarModel car)
        {
            _context.CarModels.Update(car);
            _context.SaveChanges();
        }

        public void AddGarage(GarageModel garage)
        {
            _context.Garages.Add(garage);
            _context.SaveChanges();
        }

        public void UpdateGarage(GarageModel garage, List<int> selectedCarIds)
        {
            var existingGarage = _context.Garages
                                         .Include(g => g.Cars)
                                         .FirstOrDefault(g => g.Id == garage.Id);
            if (existingGarage == null) return;

            existingGarage.Name = garage.Name;
            existingGarage.Address = garage.Address;
            existingGarage.Capacity = garage.Capacity;
            existingGarage.HasSecurity = garage.HasSecurity;
            existingGarage.IsOpen24Hours = garage.IsOpen24Hours;
            existingGarage.RentFee = garage.RentFee;

            existingGarage.Cars.Clear();
            if (selectedCarIds != null)
            {
                var cars = _context.CarModels.Where(c => selectedCarIds.Contains(c.Id)).ToList();
                foreach (var car in cars) existingGarage.Cars.Add(car);
            }

            _context.SaveChanges();
        }

        public void DeleteGarage(int id)
        {
            var garage = _context.Garages.Find(id);
            if (garage != null)
            {
                _context.Garages.Remove(garage);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CarModel> GetAllCars()
        {
            return _context.CarModels.AsNoTracking().ToList();
        }
    }
}