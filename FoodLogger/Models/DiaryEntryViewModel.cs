using FoodLogger.Data.Models;

namespace FoodLogger.Models
{
    public class DiaryEntryViewModel
    {
        public int DiaryId { get; set; }
        public DateTime DiaryDate { get; set; }
        public List<Food>? AvailableFoods { get; set; }
        public List<Recipe>? AvailableRecipes { get; set; }
        public int? SelectedFoodId { get; set; }
        public int? SelectedRecipeId { get; set; }
        public int Quantity { get; set; }
        public string AppUserId { get; set; }
    }
}
