using FoodLogger.Data.Models;

namespace FoodLogger.Models
{
    public class DiaryEntryViewModel
    {
        public int DiaryId { get; set; } // To associate with a specific diary
        public DateTime DiaryDate { get; set; }
        public List<Food>? AvailableFoods { get; set; } // List of available foods for dropdown
        public List<Recipe>? AvailableRecipes { get; set; } // List of available recipes for dropdown
        public int? SelectedFoodId { get; set; } // Property for selected food
        public int? SelectedRecipeId { get; set; } // Property for selected recipe
        public int Quantity { get; set; }
        public string AppUserId { get; set; }
    }
}
