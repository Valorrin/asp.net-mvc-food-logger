using FoodLogger.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodLogger.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Food> Foods { get; set; }
    }
}
