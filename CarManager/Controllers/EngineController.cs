using CarManager.Models;
using CarManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Controllers
{
    public class EngineController : Controller
    {
        private readonly IEngineService _engineService;

        public EngineController(IEngineService engineService)
        {
            _engineService = engineService;
        }

        public IActionResult Index() => View(_engineService.GetAllEngines());

        public IActionResult Details(int id) => View(_engineService.GetEngineById(id));

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(EngineModel engine)
        {
            if (ModelState.IsValid)
            {
                _engineService.AddEngine(engine);
                return RedirectToAction("Index");
            }

            return View(engine);
        }

        public IActionResult Edit(int id) => View(_engineService.GetEngineById(id));

        [HttpPost]
        public IActionResult Edit(EngineModel engine)
        {
            if (ModelState.IsValid)
            {
                _engineService.UpdateEngine(engine);
                return RedirectToAction("Index");
            }
            return View(engine);
        }

        public IActionResult Delete(int id) => View(_engineService.GetEngineById(id));

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _engineService.DeleteEngine(id);
            return RedirectToAction("Index");
        }
    }
}