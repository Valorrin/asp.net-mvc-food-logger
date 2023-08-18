using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Food>> GetAllUserFoods();
        Task<List<Recipe>> GetAllUserRecipes();
    }
}
