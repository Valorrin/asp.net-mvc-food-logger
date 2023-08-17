using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
        public IActionResult AddFoodEntry(string selectedDate)
        {
            DateTime.TryParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime diaryDate);

            var userId = httpContextAccessor.HttpContext.User.GetUserId();
            var diaryId = diaryRepository.GetDiaryId(userId, diaryDate.Date);
            var availableFoods = foodRepository.GetAllFoods();

            var entryViewModel = new DiaryEntryViewModel
            {
                DiaryDate = diaryDate,
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
        public IActionResult AddRecipeEntry(string selectedDate)
        {
            DateTime.TryParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime diaryDate);

            var userId = httpContextAccessor.HttpContext.User.GetUserId();
            var diaryId = diaryRepository.GetDiaryId(userId, diaryDate.Date);
            var availableRecipes = recipeRepository.GetAllRecipes();

            var entryViewModel = new DiaryEntryViewModel
            {
                DiaryDate = diaryDate,
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
                    RecipeId = entryViewModel.SelectedRecipeId,
                    Quantity = entryViewModel.Quantity,
                };

                diaryRepository.AddDiaryEntry(newDiaryEntry);

                return RedirectToAction("Index", "Diary");
            }

            entryViewModel.AvailableRecipes = recipeRepository.GetAllRecipes().ToList();
            return View(entryViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id , string selectedDate)
        {
            var entryToDelete = diaryRepository.GetDiaryEntryById(id);

            if (entryToDelete == null)
            {
                return NotFound();
            }

            diaryRepository.DeleteEntry(entryToDelete);
            return RedirectToAction("Index", "Diary", new { selectedDate }); 
        }
    }
}
