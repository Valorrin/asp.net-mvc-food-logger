using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodLogger.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IFoodRepository foodRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RecipeController(IFoodRepository foodRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.foodRepository = foodRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();
            var createFoodViewModel = new FoodViewModel { AppUserId = curUserId };
            return View(createFoodViewModel);
        }
    }
}
