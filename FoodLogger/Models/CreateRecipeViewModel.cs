using FoodLogger.Data.Models;

namespace FoodLogger.Models
{
    public class CreateRecipeViewModel
    {
        public string Name { get; set; }
        public List<int> SelectedFoodIds { get; set; }
        public List<Food> AvailableFoods { get; set; }
        public string AppUserId { get; set; }
    }
}
