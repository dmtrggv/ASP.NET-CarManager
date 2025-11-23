using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManager.Models
{
    public enum CarColor
    {
        Red, Blue, Green, Black, White, Yellow
    }

    public class CarModel
    {
        [Key, Required]
        public int Id { get; set; }

        [Display(Name = "Brand & Model")]
        public string Brand { get; set; }

        [Display(Name = "Mileage [km]")]
        public int Mileage { get; set; }

        [Display(Name = "Passengers")]
        public int CountOfPassenger { get; set; }

        [Display(Name = "Coupe color")]
        public CarColor Color { get; set; }

        [Display(Name = "Production Date")]
        public DateTime ProductionDate { get; set; }

        [Display(Name = "Deal price")]
        public decimal Price { get; set; }

        [Display(Name = "Price includes tax")]
        public bool IncludedTax { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }


        // ===== GARAGE (nullable) =====
        public int? GarageId { get; set; }

        [ForeignKey("GarageId"), Display(Name = "Garage")]
        public GarageModel? Garage { get; set; }   // <-- FIXED


        // ===== ENGINE (nullable) =====
        public int? EngineId { get; set; }

        [ForeignKey("EngineId"), Display(Name = "Engine")]
        public EngineModel? Engine { get; set; }   // <-- FIXED
    }
}
