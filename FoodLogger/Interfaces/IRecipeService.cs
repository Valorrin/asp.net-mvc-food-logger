using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IRecipeService
    {
        bool HasAssociatedDiaryEntries(Recipe food);
    }
}
