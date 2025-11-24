using CarManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<GarageModel> Garages { get; set; }
        public DbSet<EngineModel> Engines { get; set; }
    }
}