using CarManager.Models;
using CarManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarManager.Controllers
{
    public class GarageController : Controller
    {
        private readonly IGarageManagementService _garageService;

        public GarageController(IGarageManagementService garageService)
        {
            _garageService = garageService;
        }

        // Index с филтър по Name и Address
        public IActionResult Index(string name, string address)
        {
            var garages = _garageService.GetAllGarages().ToList();

            if (!string.IsNullOrWhiteSpace(name))
                garages = garages.Where(g => g.Name.ToLower().Contains(name.ToLower())).ToList();

            if (!string.IsNullOrWhiteSpace(address))
                garages = garages.Where(g => g.Address.ToLower().Contains(address.ToLower())).ToList();

            ViewBag.SelectedName = name;
            ViewBag.SelectedAddress = address;

            return View(garages);
        }

        public IActionResult Details(int id)
        {
            var garage = _garageService.GetGarageById(id);
            if (garage == null) return NotFound();
            return View(garage);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GarageModel garage)
        {
            if (ModelState.IsValid)
            {
                _garageService.AddGarage(garage);
                return RedirectToAction(nameof(Index));
            }
            return View(garage);
        }

        public IActionResult Edit(int id)
        {
            var garage = _garageService.GetGarageById(id);
            if (garage == null) return NotFound();

            ViewBag.Cars = _garageService.GetAllCars();
            return View(garage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GarageModel garage, List<int> selectedCarIds)
        {
            if (id != garage.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _garageService.UpdateGarage(garage, selectedCarIds);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cars = _garageService.GetAllCars();
            return View(garage);
        }

        public IActionResult Delete(int id)
        {
            var garage = _garageService.GetGarageById(id);
            if (garage == null) return NotFound();
            return View(garage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _garageService.DeleteGarage(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AssignCar(int id)
        {
            var garage = _garageService.GetGarageById(id);
            if (garage == null) return NotFound();

            ViewBag.GarageId = id;
            ViewBag.Cars = _garageService.GetAllCars().Where(c => c.GarageId != id).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignCar(int garageId, int carId)
        {
            var garage = _garageService.GetGarageById(garageId);
            var car = _garageService.GetAllCars().FirstOrDefault(c => c.Id == carId);

            if (garage == null || car == null) return NotFound();

            car.GarageId = garageId;
            _garageService.UpdateCar(car);

            return RedirectToAction("Details", new { id = garageId });
        }

        public IActionResult GarageCars(int id)
        {
            var garage = _garageService.GetGarageById(id);
            if (garage == null) return NotFound();

            var cars = garage.Cars.ToList();
            ViewBag.GarageName = garage.Name;
            return View(cars);
        }
    }
}
