using FoodLogger.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FoodLogger.Data.DataConstants;

namespace FoodLogger.Data.Models
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Range(GramsMinValue, GramsMaxValue)]
        public double Grams { get; set; }

        [Range(CaloriesMinValue, CaloriesMaxValue)]
        public double Calories { get; set; }

        [Range(ProteinMinValue, ProteinMaxValue)]
        public double Protein { get; set; }

        [Range(CarbsMinValue, CarbsMaxValue)]
        public double Carbs { get; set; }

        [Range(FatMinValue, FatMaxValue)]
        public double Fat { get; set; }

        public  FoodCategory FoodCategory { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
