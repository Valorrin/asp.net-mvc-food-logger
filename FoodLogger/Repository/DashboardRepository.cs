using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;

namespace FoodLogger.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private ApplicationDbContext context;
        private DashboardRepository dashboardRepository;
        private IHttpContextAccessor httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, DashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.dashboardRepository = dashboardRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<List<Food>> GetAllUserFoods()
        {
            throw new NotImplementedException();
        }
    }
}
