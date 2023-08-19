using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IRecipeRepository
    {
        public bool Create(Recipe recipe);

        public bool Update(Recipe recipe);

        public bool Delete(Recipe recipe);

        public IEnumerable<Recipe> GetAllRecipes();

        public Task<IEnumerable<Recipe>> GetAllAsync();

        public Task<IEnumerable<Recipe>> GetAllRecipesForUser(string appUserId);

        public Task<Recipe> GetByIdAsync(int id);

        public Recipe GetById(int id);

        public Ingredient CreateIngredient(Food food);

        public bool Save();
    }
}
