using CarManager.Data;
using CarManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarManager.Services
{
    public class EngineService : IEngineService
    {
        private readonly ApplicationDbContext _context;

        public EngineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EngineModel> GetAllEngines() => _context.Engines.ToList();
        public EngineModel GetEngineById(int id) => _context.Engines.Find(id);

        public void AddEngine(EngineModel engine)
        {
            _context.Engines.Add(engine);
            _context.SaveChanges();
        }

        public void UpdateEngine(EngineModel engine)
        {
            _context.Engines.Update(engine);
            _context.SaveChanges();
        }

        public void DeleteEngine(int id)
        {
            var engine = _context.Engines.Find(id);
            if (engine != null)
            {
                _context.Engines.Remove(engine);
                _context.SaveChanges();
            }
        }
    }
}
