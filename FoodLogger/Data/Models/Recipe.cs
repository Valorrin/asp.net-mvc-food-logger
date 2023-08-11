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

        public ICollection<Food>? FoodItems { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        [NotMapped]
        public double Grams => FoodItems.Sum(fi => fi.Grams);

        [NotMapped]
        public double Calories => FoodItems.Sum(fi => fi.Calories);

        [NotMapped]
        public double Protein => FoodItems.Sum(fi => fi.Protein);

        [NotMapped]
        public double Carbohydrates => FoodItems.Sum(fi => fi.Protein);

        [NotMapped]
        public double Fat => FoodItems.Sum(fi => fi.Protein);


    }
}
