using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Food food)
        {
            if (!ModelState.IsValid)
            {
                return View(food);
            }

            foodRepository.Create(food);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var food = await foodRepository.GetById(id);
            if (food == null) { return View("Error"); }

            var foodVM = new EditFoodViewModel
            {
                Id = food.Id,
                Name = food.Name,
                Grams = food.Grams,
                Calories = food.Calories,
                Carbs = food.Carbs,
                Protein = food.Protein,
                Fat = food.Fat,
                FoodCategory = food.FoodCategory

            };

            return View(foodVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFoodViewModel foodVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", foodVM);
            }

            var food = new Food
            {
                Id = foodVM.Id,
                Name = foodVM.Name,
                Grams = foodVM.Grams,
                Calories = foodVM.Calories,
                Carbs = foodVM.Carbs,
                Protein = foodVM.Protein,
                Fat = foodVM.Fat,
                FoodCategory = foodVM.FoodCategory
                              
            };

            foodRepository.Update(food);

            return RedirectToAction("Index");
        }
    }
}
