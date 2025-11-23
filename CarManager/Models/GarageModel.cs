using System.ComponentModel.DataAnnotations;

namespace CarManager.Models
{
    public class GarageModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Garage name")]
        public string Name { get; set; }

        [Required, Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Car capacity")]
        public int Capacity { get; set; }

        [Display(Name = "Has security")]
        public bool HasSecurity { get; set; }

        [Display(Name = "Opened 24/7")]
        public bool IsOpen24Hours { get; set; }

        [Display(Name = "Monthly fee")]
        public decimal RentFee { get; set; }

        [Display(Name = "Cars")]
        public ICollection<CarModel> Cars { get; set; } = new List<CarModel>();
    }
}
