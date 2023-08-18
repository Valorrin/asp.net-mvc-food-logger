using FoodLogger.Data.Enum;

namespace FoodLogger.Models.ExternalData
{
    public class EdFoodViewModel
    {
        public string Name { get; set; }

        public double Grams { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Carbs { get; set; }

        public double Fat { get; set; }

        public string? AppUserId { get; set; }

        public int? RecipeId { get; set; }
    }
}
