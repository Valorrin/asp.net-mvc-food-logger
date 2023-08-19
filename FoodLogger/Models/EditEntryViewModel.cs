using System.ComponentModel.DataAnnotations;

namespace FoodLogger.Models
{
    public class EditEntryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public double Quantity { get; set; }
    }
}
