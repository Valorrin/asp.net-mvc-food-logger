using FoodLogger.Data;
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
    }
}
