using FoodLogger.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FoodLogger.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeFood> RecipesFoods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RecipeFood>()
            .HasKey(rf => new { rf.RecipeId, rf.FoodId });
        }
    }
}
