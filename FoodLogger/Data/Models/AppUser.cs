using Microsoft.AspNetCore.Identity;

namespace FoodLogger.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
