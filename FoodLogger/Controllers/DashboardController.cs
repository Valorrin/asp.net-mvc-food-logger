using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodLogger.Controllers
{
    public class DashboardController : Controller
    {
        private IDashboardRepository dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userFoods = await dashboardRepository.GetAllUserFoods(appUserId);
            var userRecipes = await dashboardRepository.GetAllUserRecipes(appUserId);

            var dashboardViewModel = new DashboardViewModel()
            {
                Foods = userFoods,
                Recipes = userRecipes,
            };

            return View(dashboardViewModel);
        }

    }
}
