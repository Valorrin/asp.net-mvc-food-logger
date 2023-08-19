using FoodLogger.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodLogger.Models
{
    public class EditFoodViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Grams is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Grams must be greater than 0")]
        public double Grams { get; set; }

        [Required(ErrorMessage = "Calories are required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Calories must be non-negative")]
        public double Calories { get; set; }

        [Required(ErrorMessage = "Protein is required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Protein must be non-negative")]
        public double Protein { get; set; }

        [Required(ErrorMessage = "Carbs are required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Carbs must be non-negative")]
        public double Carbs { get; set; }

        [Required(ErrorMessage = "Fat is required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Fat must be non-negative")]
        public double Fat { get; set; }
    }
}
