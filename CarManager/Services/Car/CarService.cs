using CarManager.Data;
using CarManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarManager.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarModel> GetAllCars() => _context.CarModels
                                                     .Include(c => c.Engine)
                                                     .Include(c => c.Garage)
                                                     .ToList();

        public CarModel GetCarById(int id) => _context.CarModels.Find(id);

        public void AddCar(CarModel car)
        {
            _context.CarModels.Add(car);
            _context.SaveChanges();
        }

        public void UpdateCar(CarModel car)
        {
            _context.CarModels.Update(car);
            _context.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            var car = _context.CarModels.Find(id);
            if (car != null)
            {
                _context.CarModels.Remove(car);
                _context.SaveChanges();
            }
        }

        public IEnumerable<EngineModel> GetAllEngines() => _context.Engines.ToList();
    }
}
