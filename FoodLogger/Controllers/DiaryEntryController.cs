using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult AddFoodEntry(DateTime selectedDate)
        {
            var userId = httpContextAccessor.HttpContext.User.GetUserId();
            var diaryId = diaryRepository.GetDiaryId(userId, selectedDate.Date);
            var availableFoods = foodRepository.GetAllFoods();

            var entryViewModel = new DiaryEntryViewModel
            {
                DiaryDate = selectedDate,
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
                var food = foodRepository.GetById(entryViewModel.SelectedFoodId.Value);

                var calories = (food.Calories / 100) * entryViewModel.Quantity;
                var protein = (food.Protein / 100) * entryViewModel.Quantity;
                var carbs = (food.Carbs / 100) * entryViewModel.Quantity;
                var fat = (food.Fat / 100) * entryViewModel.Quantity;

                var newDiaryEntry = new DiaryEntry
                {
                    DiaryId = entryViewModel.DiaryId,
                    FoodId = entryViewModel.SelectedFoodId,
                    Quantity = entryViewModel.Quantity,
                    Calories = calories,
                    Protein = protein,
                    Carbs = carbs,
                    Fats = fat,
                };
            
                diaryRepository.AddDiaryEntry(newDiaryEntry);

                return RedirectToAction("Index", "Diary", new { selectedDate = entryViewModel.DiaryDate });
            }

            entryViewModel.AvailableFoods = foodRepository.GetAllFoods().ToList();
            return View(entryViewModel);
        }

        [HttpGet]
        public IActionResult AddRecipeEntry(DateTime selectedDate)
        {
          //  DateTime.TryParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime diaryDate);

            var userId = httpContextAccessor.HttpContext.User.GetUserId();
            var diaryId = diaryRepository.GetDiaryId(userId, selectedDate.Date);
            var availableRecipes = recipeRepository.GetAllRecipes();

            var entryViewModel = new DiaryEntryViewModel
            {
                DiaryDate = selectedDate,
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

                return RedirectToAction("Index", "Diary", new { selectedDate = entryViewModel.DiaryDate.Date });
            }

            entryViewModel.AvailableRecipes = recipeRepository.GetAllRecipes().ToList();
            return View(entryViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entry = diaryRepository.GetDiaryEntryById(id);

            if (entry == null)
            {
                return NotFound();
            }

            EditEntryViewModel entryViewModel;

            if (entry.FoodId != null)
            {
                entryViewModel = new EditEntryViewModel
                {
                    Id = id,
                    Quantity = entry.Quantity,
                   // Name = entry.Food.Name,
                };
            }
            else
            {
                entryViewModel = new EditEntryViewModel
                {
                    Id = id,
                    Quantity = entry.Quantity,
                  //  Name = entry.Recipe.Name,
                };
            }


            return View(entryViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entry = diaryRepository.GetDiaryEntryById(model.Id);

            if (entry == null)
            {
                return NotFound();
            }
            
            var calories = (entry.Calories / entry.Quantity) * model.Quantity;
            var protein = (entry.Protein / entry.Quantity) * model.Quantity;
            var carbs = (entry.Carbs / entry.Quantity) * model.Quantity;
            var fat = (entry.Fats / entry.Quantity) * model.Quantity;

            entry.Quantity = model.Quantity;
            entry.Calories = calories;
            entry.Protein = protein;
            entry.Carbs = carbs;
            entry.Fats = fat;

            diaryRepository.Update(entry);

            return RedirectToAction("Index", "Diary");
        }

        [HttpPost]
        public IActionResult Delete(int id , DateTime selectedDate)
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
