using FoodLogger.Data.Models;
using FoodLogger.Models;

namespace FoodLogger.Interfaces
{
    public interface IFoodRepository
    {
        public bool Create(Food food);


        public bool Delete(Food food);


        public  Task<IEnumerable<Food>> GetAll();

        public IEnumerable<Food> GetAllFoods();


        public  Food GetById(int id);

        public bool Update(Food food);


        public bool Save();

    }
}
