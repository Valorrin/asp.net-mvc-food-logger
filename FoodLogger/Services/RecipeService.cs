using FoodLogger.Data.Models;
using FoodLogger.Interfaces;

namespace FoodLogger.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IDiaryRepository diaryRepository;

        public RecipeService(IDiaryRepository diaryRepository)
        {
            this.diaryRepository = diaryRepository;
        }

        public bool HasAssociatedDiaryEntries(Recipe recipe)
        {
            return diaryRepository.GetAllEntriesByRecipeId(recipe.Id).Any();
        }
    }
}
