using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodLogger.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext context;

        public RecipeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Create(Recipe recipe)
        {
            context.Add(recipe);
            return Save();
        }

        public bool Update(Recipe recipe)
        {
            context.Update(recipe);
            return Save();
        }

        public bool Delete(Recipe recipe)
        {
            context.Remove(recipe);
            return Save();
        }

        public Recipe GetById(int id)
        {
            return context.Recipes.Include(r => r.Foods).FirstOrDefault(x => x.Id == id);
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            return await context.Recipes.Include(r => r.Foods).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await context.Recipes.Include(r => r.Foods).ToListAsync();
        }

        public IEnumerable<Recipe> GetAllRecipes()
        { 
            return context.Recipes.Include(r => r.Foods).ToList();
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesForUser(string appUserId)
        {
            return await context.Recipes.Include(r => r.Foods).Where(r => r.AppUserId == appUserId).ToListAsync();
        }

        public Ingredient CreateIngredient(Food food)
        {
            var newIngredient = new Ingredient
            {
                Name = food.Name,
                Calories = food.Calories,
                Grams = food.Grams,
                Protein = food.Protein,
                Carbs = food.Carbs,
                Fat = food.Fat,
            };
            return newIngredient;
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
