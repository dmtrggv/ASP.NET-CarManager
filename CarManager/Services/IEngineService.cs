using CarManager.Models;
using System.Collections.Generic;

namespace CarManager.Services
{
    public interface IEngineService
    {
        IEnumerable<EngineModel> GetAllEngines();
        EngineModel GetEngineById(int id);
        void AddEngine(EngineModel engine);
        void UpdateEngine(EngineModel engine);
        void DeleteEngine(int id);
    }
}
