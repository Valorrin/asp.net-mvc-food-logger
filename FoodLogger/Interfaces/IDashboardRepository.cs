using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Food>> GetAllUserFoods(string userId);
        Task<List<Recipe>> GetAllUserRecipes(string userId);
    }
}
