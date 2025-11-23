using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Models
{
    public enum EngineType
    {
        Petrol, Diesel, Electric, Hybrid, Hydrogen, Biofuel, Gas, Other
    }

    public class EngineModel
    {
        public int Id { get; set; }

        [Display(Name = "Engine code")]
        public string Code { get; set; }

        [Display(Name = "Engine Type")]
        public EngineType TypeEngine { get; set; }

        [Display(Name = "Start production date")]
        public DateTime DateStarted { get; set; }

        [Display(Name = "End production date")]
        public DateTime DateEnded { get; set; }

        [Display(Name = "Manufacturer")]
        public string? Manufacturer { get; set; }

        public ICollection<CarModel> Cars { get; set; } = new List<CarModel>();
    }
}
