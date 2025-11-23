using CarManager.Models;
using CarManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CarManager.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IGarageManagementService _garageService;

        public CarController(ICarService carService, IGarageManagementService garageService)
        {
            _carService = carService;
            _garageService = garageService;
        }

        public IActionResult Index(string brand, CarColor? color)
        {
            var cars = _carService.GetAllCars().ToList();

            if (!string.IsNullOrWhiteSpace(brand))
                cars = cars.Where(c => c.Brand.ToLower().Contains(brand.ToLower())).ToList();

            if (color.HasValue)
                cars = cars.Where(c => c.Color == color).ToList();

            ViewBag.Garages = new SelectList(_garageService.GetAllGarages(), "Id", "Name");
            ViewBag.SelectedBrand = brand;
            ViewBag.SelectedColor = color;

            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();

            ViewBag.Garages = new SelectList(_garageService.GetAllGarages(), "Id", "Name", car.GarageId);
            ViewBag.Engines = new SelectList(_carService.GetAllEngines(), "Id", "Code", car.EngineId);

            return View(car);
        }

        #region Create

        public IActionResult Create()
        {
            ViewBag.Garages = new SelectList(_garageService.GetAllGarages(), "Id", "Name");
            ViewBag.Engines = new SelectList(_carService.GetAllEngines(), "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel car)
        {
            if (ModelState.IsValid)
            {
                car.Garage = null;
                car.Engine = null;

                _carService.AddCar(car);
                return RedirectToAction("Index");
            }

            ViewBag.Garages = new SelectList(_garageService.GetAllGarages(), "Id", "Name", car.GarageId);
            ViewBag.Engines = new SelectList(_carService.GetAllEngines(), "Id", "Code", car.EngineId);
            return View(car);
        }

        #endregion

        #region Edit

        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();

            ViewBag.Garages = new SelectList(_garageService.GetAllGarages(), "Id", "Name", car.GarageId);
            ViewBag.Engines = new SelectList(_carService.GetAllEngines(), "Id", "Code", car.EngineId);

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarModel car)
        {
            if (id != car.Id) return BadRequest();

            _carService.UpdateCar(car);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
