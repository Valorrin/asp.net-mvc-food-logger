using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetAll();

        Task<Food> GetById(int id);

        bool Create(Food food);

        bool Update(Food food);

        bool Delete(Food food);

        bool Save();


    }
}
