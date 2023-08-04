using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodLogger.Controllers
{
    public class DashboardController : Controller
    {
        private IDashboardRepository dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userFoods = await dashboardRepository.GetAllUserFoods();

            var dashboardViewModel = new DashboardViewModel()
            {
                Foods = userFoods,
            };

            return View(dashboardViewModel);
        }
    }
}
