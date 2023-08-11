using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IRecipeRepository
    {
        public bool Create(Recipe recipe);


        public bool Delete(Recipe recipe);


        public Task<IEnumerable<Recipe>> GetAll();


        public Task<Recipe> GetById(int id);

        public bool Update(Recipe recipe);


        public bool Save();
    }
}
