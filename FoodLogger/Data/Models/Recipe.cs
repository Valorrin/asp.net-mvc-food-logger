using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FoodLogger.Data.DataConstants;

namespace FoodLogger.Data.Models
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public List<Food> Foods { get; set; } = new List<Food>();

        public ICollection<DiaryEntry> DiaryEntries { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [NotMapped]
        public double CalculateGrams => Foods.Sum(food => food.Grams);
        [NotMapped]
        public double CalculateCalories => Foods.Sum(food => food.Calories);
        [NotMapped]
        public double CalculateProtein => Foods.Sum(food => food.Protein);
        [NotMapped]
        public double CalculateCarbs => Foods.Sum(food => food.Carbs);
        [NotMapped]
        public double CalculateFats => Foods.Sum(food => food.Fat);

    }
}
