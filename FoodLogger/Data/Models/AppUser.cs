using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FoodLogger.Data.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Food> Foods { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Diary> Diaries { get; set; }
    }
}
