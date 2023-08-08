using FoodLogger.Data;
using FoodLogger.Interfaces;
using FoodLogger.Models;

namespace FoodLogger.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext context;

        public StatisticsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public StatisticsRepositoryModel Total()
        {
            var totalUsers = context.Users.Count();
            var totalFoods = context.Foods.Count();

            return new StatisticsRepositoryModel
            {
                TotalUsers = totalUsers,
                TotalFoods = totalFoods
            };
        }
    }
}
