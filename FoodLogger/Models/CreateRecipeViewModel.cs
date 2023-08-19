using FoodLogger.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodLogger.Models
{
    public class CreateRecipeViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public List<int> SelectedFoodIds { get; set; }
        public List<Food> AvailableFoods { get; set; }

        [Required(ErrorMessage = "AppUserId is required")]
        public string AppUserId { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Calories must be non-negative")]
        public double Calories { get; set; }
    }
}
