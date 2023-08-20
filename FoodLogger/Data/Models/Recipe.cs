using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static FoodLogger.Data.DataConstants;

namespace FoodLogger.Data.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Foods = new List<Ingredient>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Ingredient> Foods { get; set; }

        public virtual ICollection<DiaryEntry> DiaryEntries { get; set; }

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
