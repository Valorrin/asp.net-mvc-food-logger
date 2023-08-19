using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodLogger.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        private readonly IFoodRepository foodRepository;
        private readonly IDiaryRepository diaryRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FoodController(IFoodRepository foodRepository, IHttpContextAccessor httpContextAccessor, IDiaryRepository diaryRepository)
        {
            this.foodRepository = foodRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.diaryRepository = diaryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await foodRepository.GetAll();

            return View(foods);
        }

        public async Task<IActionResult> GetFoodsPartial()
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var foods = await foodRepository.GetAllFoodsForUser(appUserId);
            return PartialView("_FoodsPartial", foods);
        }

        public IActionResult Detail(int id)
        {
            var food = foodRepository.GetById(id);

            return View(food);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();
            var createFoodViewModel = new FoodViewModel { AppUserId = curUserId };
            return View(createFoodViewModel);
        }

        [HttpPost]
        public IActionResult Create(FoodViewModel foodVM)
        {
            if (!ModelState.IsValid)
            {
                return View(foodVM);
            }

            double caloriesPer100Grams = (foodVM.Calories / foodVM.Grams) * 100;
            double proteinPer100Grams = (foodVM.Protein / foodVM.Grams) * 100;
            double carbsPer100Grams = (foodVM.Carbs / foodVM.Grams) * 100;
            double fatPer100Grams = (foodVM.Fat / foodVM.Grams) * 100;

            var food = new Food
            {
                Id = foodVM.Id,
                Name = foodVM.Name,
                Grams = 100,
                Calories = caloriesPer100Grams,
                Protein = proteinPer100Grams,
                Carbs = carbsPer100Grams,
                Fat = fatPer100Grams,
                AppUserId = foodVM.AppUserId,
            };

            foodRepository.Create(food);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var food = foodRepository.GetById(id);
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
            };

            return View(foodVM);
        }

        [HttpPost]
        public IActionResult Edit(EditFoodViewModel foodVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", foodVM);
            }

            double caloriesPer100Grams = (foodVM.Calories / foodVM.Grams) * 100;
            double proteinPer100Grams = (foodVM.Protein / foodVM.Grams) * 100;
            double carbsPer100Grams = (foodVM.Carbs / foodVM.Grams) * 100;
            double fatPer100Grams = (foodVM.Fat / foodVM.Grams) * 100;

            var food = new  Food
            {
                Id = foodVM.Id,
                Name = foodVM.Name,
                Grams = 100,
                Calories = caloriesPer100Grams,
                Carbs = carbsPer100Grams,
                Protein = proteinPer100Grams,
                Fat = fatPer100Grams,
                              
            };

            foodRepository.Update(food);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public ActionResult Delete(int id) 
        {
            var food = foodRepository.GetById(id);
            if (food == null) { return View("Error"); }

            if (HasAssociatedDiaryEntries(food))
            {
                TempData["ErrorMessage"] = "Cannot delete this food. It is associated with diary entry.";
                return RedirectToAction("Index", "Dashboard");
            }

            foodRepository.Delete(food);
            return RedirectToAction("Index", "Dashboard");
        }

        private bool HasAssociatedDiaryEntries(Food food)
        {
           
            return diaryRepository.GetAllEntriesByFoodId(food.Id).Any();
        }

    }
}
