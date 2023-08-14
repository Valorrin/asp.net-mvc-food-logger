namespace FoodLogger.Data.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public int DiaryId { get; set; }
        public int? RecipeId { get; set; }
        public int? FoodId { get; set; }

        public Diary Diary { get; set; }
        public Recipe Recipe { get; set; }
        public Food Food { get; set; }
    }
}
