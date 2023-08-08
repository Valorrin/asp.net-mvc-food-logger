using FoodLogger.Models;

namespace FoodLogger.Interfaces
{
    public interface IStatisticsRepository
    {
        StatisticsRepositoryModel Total();
    }
}
