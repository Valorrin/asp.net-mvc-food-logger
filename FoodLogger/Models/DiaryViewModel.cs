using FoodLogger.Data.Models;

namespace FoodLogger.Models
{
    public class DiaryViewModel
    {
        public int DiaryId { get; set; }
        public DateTime? SelectedDate { get; set; }
        public List<DiaryEntry> DiaryEntries { get; set; }

        public double Grams { get; set; }
        public double TotalCalories { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFats { get; set; }
    }
}
