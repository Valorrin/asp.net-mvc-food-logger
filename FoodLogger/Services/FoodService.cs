using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Repository;

namespace FoodLogger.Services
{
    public class FoodService : IFoodService
    {
        private readonly IDiaryRepository diaryRepository;

        public FoodService(IDiaryRepository diaryRepository)
        {
            this.diaryRepository = diaryRepository;
        }

        public bool HasAssociatedDiaryEntries(Food food)
        {
            return diaryRepository.GetAllEntriesByFoodId(food.Id).Any();
        }
    }
}
