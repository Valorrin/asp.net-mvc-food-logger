using FoodLogger.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodLogger.Models
{
    public class EditFoodViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Grams { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Carbs { get; set; }

        public double Fat { get; set; }

        public virtual FoodCategory? FoodCategory { get; set; }
    }
}
