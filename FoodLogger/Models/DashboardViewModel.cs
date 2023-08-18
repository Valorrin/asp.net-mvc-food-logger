using FoodLogger.Data.Models;
using System.Runtime.InteropServices;

namespace FoodLogger.Models
{
    public class DashboardViewModel
    {
        public List<Food> Foods { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
