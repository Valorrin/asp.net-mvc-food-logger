using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodLogger.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext context;

        public FoodRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Create(Food food)
        {
            context.Foods.Add(food);
            return Save();
        }

        public bool Delete(Food food)
        {
            context.Foods.Remove(food);
            return Save();
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            return await context.Foods.ToListAsync();
        }

        public IEnumerable<Food> GetAllFoods()
        {
            return context.Foods.ToList();
        }

        public Food GetById(int id)
        {
            return  context.Foods.FirstOrDefault(f => f.Id == id);
        }
        public bool Update(Food food)
        {
            context.Foods.Update(food);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Food>> GetAllFoodsForUser(string appUserId)
        {
            return await context.Foods.Where(f => f.AppUserId == appUserId).ToListAsync();
        }
    }
}
