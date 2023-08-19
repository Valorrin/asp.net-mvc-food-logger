using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IFoodService
    {
        bool HasAssociatedDiaryEntries(Food food);
    }
}
