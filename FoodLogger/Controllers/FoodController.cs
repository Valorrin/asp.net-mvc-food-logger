using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodLogger.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodRepository foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await foodRepository.GetAll();

            return View(foods);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var food = await foodRepository.GetById(id);

            return View(food);
        }
    }
}
