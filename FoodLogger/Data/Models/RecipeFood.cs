namespace FoodLogger.Data.Models
{
    public class RecipeFood
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
