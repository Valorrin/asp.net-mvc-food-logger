using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLogger.Data.Models
{
    public class Diary
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<DiaryEntry> DiaryEntries { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
