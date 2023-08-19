using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodLogger.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private ApplicationDbContext context;

        public DashboardRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Food>> GetAllUserFoods(string curUserId)
        {
            var userFoods = context.Foods.Where(f => f.AppUser.Id == curUserId);      
            
            return userFoods.ToList();
        }

        public async Task<List<Recipe>> GetAllUserRecipes(string curUserId)
        {
            var userRecipes = context.Recipes.Include(r => r.Foods).Where(r => r.AppUser.Id == curUserId);

            return userRecipes.ToList();
        }
    }
}
