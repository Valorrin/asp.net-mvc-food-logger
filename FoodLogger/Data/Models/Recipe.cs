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

        public double CalculateGrams => Foods.Sum(food => food.Grams);
        public double CalculateCalories => Foods.Sum(food => food.Calories);
        public double CalculateProtein => Foods.Sum(food => food.Protein);
        public double CalculateCarbs => Foods.Sum(food => food.Carbs);
        public double CalculateFats => Foods.Sum(food => food.Fat);

    }
}
