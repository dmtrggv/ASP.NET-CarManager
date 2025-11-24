using CarManager.Models;
using CarManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarManager.Controllers
{
    public class EngineController : Controller
    {
        private readonly IEngineService _engineService;

        public EngineController(IEngineService engineService)
        {
            _engineService = engineService;
        }

        public IActionResult Index(string code, EngineType? type)
        {
            var engines = _engineService.GetAllEngines().ToList();

            // Filter by Code
            if (!string.IsNullOrWhiteSpace(code))
                engines = engines.Where(e => e.Code.ToLower().Contains(code.ToLower())).ToList();

            // Filter by EngineType
            if (type.HasValue)
                engines = engines.Where(e => e.TypeEngine == type).ToList();

            ViewBag.SelectedCode = code;
            ViewBag.SelectedType = type;

            return View(engines);
        }

        public IActionResult Details(int id)
        {
            var engine = _engineService.GetEngineById(id);
            if (engine == null) return NotFound();
            return View(engine);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EngineModel engine)
        {
            if (ModelState.IsValid)
            {
                _engineService.AddEngine(engine);
                return RedirectToAction("Index");
            }
            return View(engine);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var engine = _engineService.GetEngineById(id);
            if (engine == null) return NotFound();
            return View(engine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EngineModel engine)
        {
            if (ModelState.IsValid)
            {
                _engineService.UpdateEngine(engine);
                return RedirectToAction("Index");
            }
            return View(engine);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var engine = _engineService.GetEngineById(id);
            if (engine == null) return NotFound();
            return View(engine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _engineService.DeleteEngine(id);
            return RedirectToAction("Index");
        }
    }
}
