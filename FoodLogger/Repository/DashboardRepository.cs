using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodLogger.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private ApplicationDbContext context;
        private IHttpContextAccessor httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Food>> GetAllUserFoods()
        {
            var curUser = httpContextAccessor.HttpContext?.User.GetUserId();
            var userFoods = context.Foods.Where(f => f.AppUser.Id == curUser.ToString());
            
            return userFoods.ToList();
        }
    }
}
