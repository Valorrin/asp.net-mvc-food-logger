using FoodLogger.Data;
using FoodLogger.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodLogger.Controllers
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext context;

        public FoodController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var foods = context.Foods.ToList();
            return View(foods);
        }

        public IActionResult Detail(int id)
        {
            Food food = context.Foods.FirstOrDefault(f => f.Id == id);
            return View(food);
        }
    }
}
