using FoodLogger.Data.Models;

namespace FoodLogger.Models
{
    public class DiaryViewModel
    {
        public DateTime SelectedDate { get; set; }
        public List<DiaryEntry> DiaryEntries { get; set; }
    }
}
