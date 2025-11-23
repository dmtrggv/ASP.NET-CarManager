using CarManager.Models;
using CarManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Controllers
{
    public class GarageController : Controller
    {
        private readonly IGarageManagementService _garageService;

        public GarageController(IGarageManagementService garageService)
        {
            _garageService = garageService;
        }

        public IActionResult Index()
        {
            return View(_garageService.GetAllGarages());
        }

        public IActionResult Details(int id)
        {
            var garage = _garageService.GetGarageById(id);
            if (garage == null) return NotFound();
            return View(garage);
        }

        public IActionResult Create()
        {
            return View();
        }

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