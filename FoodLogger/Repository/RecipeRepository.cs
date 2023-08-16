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

        public bool Delete(Recipe recipe)
        {
            context.Remove(recipe);
            return Save();
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            return await context.Recipes.ToListAsync();
        }
        public IEnumerable<Recipe> GetAllRecipes()
        { 
            return context.Recipes.ToList();
        }

        public async Task<Recipe> GetById(int id)
        {
            return await context.Recipes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Update(Recipe recipe)
        {
            context.Update(recipe);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
