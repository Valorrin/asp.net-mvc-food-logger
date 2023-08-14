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

        public ICollection<RecipeFood>? RecipeFoods { get; set; }
        public ICollection<DiaryEntry> DiaryEntries { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        [NotMapped]
        public double Grams => RecipeFoods.Sum(fi => fi.Food.Grams);

        [NotMapped]
        public double Calories => RecipeFoods.Sum(fi => fi.Food.Calories);

        [NotMapped]
        public double Protein => RecipeFoods.Sum(fi => fi.Food.Protein);

        [NotMapped]
        public double Carbohydrates => RecipeFoods.Sum(fi => fi.Food.Protein);

        [NotMapped]
        public double Fat => RecipeFoods.Sum(fi => fi.Food.Protein);


    }
}
