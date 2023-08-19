namespace FoodLogger.Data.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public int DiaryId { get; set; }
        public int? RecipeId { get; set; }
        public int? FoodId { get; set; }
        public double Quantity { get; set; }

        public Diary Diary { get; set; }
        public Recipe Recipe { get; set; }
        public Food Food { get; set; }

        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }

    }
}
