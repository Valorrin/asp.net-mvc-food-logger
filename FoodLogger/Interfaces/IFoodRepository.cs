using FoodLogger.Data.Models;
using FoodLogger.Models;

namespace FoodLogger.Interfaces
{
    public interface IFoodRepository
    {
        public bool Create(Food food);

        public bool Update(Food food);

        public bool Delete(Food food);

        public IEnumerable<Food> GetAllFoods();

        public  Task<IEnumerable<Food>> GetAllAsync();

        public Task<IEnumerable<Food>>GetAllFoodsForUser(string appUserId);

        public  Food GetById(int id);

        public bool Save();

    }
}
