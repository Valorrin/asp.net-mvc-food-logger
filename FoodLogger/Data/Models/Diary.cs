using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLogger.Data.Models
{
    public class Diary
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Add foreign key relationship to User
        public DateTime Date { get; set; }
        public ICollection<DiaryEntry> DiaryEntries { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
