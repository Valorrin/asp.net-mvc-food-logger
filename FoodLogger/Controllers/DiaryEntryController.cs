using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodLogger.Controllers
{
    public class DiaryEntryController : Controller
    {
        private readonly IFoodRepository foodRepository;
        private readonly IRecipeRepository recipeRepository;
        private readonly IDiaryRepository diaryRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DiaryEntryController(IFoodRepository foodRepository, IRecipeRepository recipeRepository, 
            IDiaryRepository diaryRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.foodRepository = foodRepository;
            this.recipeRepository = recipeRepository;
            this.diaryRepository = diaryRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult AddFoodEntry()
        {
            var userId = httpContextAccessor.HttpContext.User.GetUserId();
            var diaryId = diaryRepository.GetDiaryId(userId, DateTime.Today);
            var availableFoods = foodRepository.GetAllFoods();

            var entryViewModel = new DiaryEntryViewModel
            {
                EntryDate = DateTime.Today,
                AvailableFoods = availableFoods.ToList(),
                AppUserId = userId,
                DiaryId = diaryId
            };

            return View(entryViewModel);
        }

        [HttpPost]
        public IActionResult AddFoodEntry(DiaryEntryViewModel entryViewModel)
        {
            if (ModelState.IsValid)
            {
                var newDiaryEntry = new DiaryEntry
                {
                    DiaryId = entryViewModel.DiaryId,
                    EntryDate = entryViewModel.EntryDate,
                    FoodId = entryViewModel.SelectedFoodId,
                    Quantity = entryViewModel.Quantity,
                };

                diaryRepository.AddDiaryEntry(newDiaryEntry);

                return RedirectToAction("Index", "Diary");
            }

            entryViewModel.AvailableFoods = foodRepository.GetAllFoods().ToList();
            return View(entryViewModel);
        }

        [HttpGet]
        public IActionResult AddRecipeEntry()
        {
            var userId = httpContextAccessor.HttpContext.User.GetUserId();
            var diaryId = diaryRepository.GetDiaryId(userId, DateTime.Today);
            var availableRecipes = recipeRepository.GetAllRecipes();

            var entryViewModel = new DiaryEntryViewModel
            {
                EntryDate = DateTime.Today,
                AvailableRecipes = availableRecipes.ToList(),
                AppUserId = userId,
                DiaryId = diaryId
            };

            return View(entryViewModel);
        }

        [HttpPost]
        public IActionResult AddRecipeEntry(DiaryEntryViewModel entryViewModel)
        {
            if (ModelState.IsValid)
            {
                var newDiaryEntry = new DiaryEntry
                {
                    DiaryId = entryViewModel.DiaryId,
                    EntryDate = entryViewModel.EntryDate,
                    RecipeId = entryViewModel.SelectedRecipeId,
                    Quantity = entryViewModel.Quantity,
                };

                diaryRepository.AddDiaryEntry(newDiaryEntry);

                return RedirectToAction("Index", "Diary");
            }

            entryViewModel.AvailableRecipes = recipeRepository.GetAllRecipes().ToList();
            return View(entryViewModel);
        }
    }
}
