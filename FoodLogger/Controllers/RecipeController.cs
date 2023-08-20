using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodLogger.Controllers
{

    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IFoodRepository foodRepository;
        private readonly IDiaryRepository diaryRepository;
        private readonly IRecipeService recipeService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RecipeController(IRecipeRepository recipeRepository, IHttpContextAccessor httpContextAccessor, 
            IFoodRepository foodRepository, IDiaryRepository diaryRepository, IRecipeService recipeService)
        {
            this.recipeRepository = recipeRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.foodRepository = foodRepository;
            this.diaryRepository = diaryRepository;
            this.recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recipes = await recipeRepository.GetAllAsync();

            return View(recipes);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipesPartial()
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipes = await recipeRepository.GetAllRecipesForUser(appUserId);

            return PartialView("_RecipesPartial", recipes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();
            var availableFoods = await foodRepository.GetAllFoodsForUser(curUserId);

            var createRecipeViewModel = new CreateRecipeViewModel
            {
                AppUserId = curUserId,
                AvailableFoods = (List<Food>)availableFoods,
                SelectedFoodIds = new List<int>()
                
            };
            return View(createRecipeViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateRecipeViewModel createRecipeViewModel)
        {
            if (!ModelState.IsValid)
            {
                createRecipeViewModel.AvailableFoods = foodRepository.GetAllFoods().ToList();
                return View(createRecipeViewModel);
            }

            var newRecipe = new Recipe
            {
                Name = createRecipeViewModel.Name,
                AppUserId = createRecipeViewModel.AppUserId,
            };

            foreach (var foodId in createRecipeViewModel.SelectedFoodIds)
            {
                var food = foodRepository.GetById(foodId);
                var ingredient = recipeRepository.CreateIngredient(food);
                if (ingredient != null)
                {
                    newRecipe.Foods.Add(ingredient);
                }
            }

            recipeRepository.Create(newRecipe);

            TempData["RecipeAdded"] = true;

            return RedirectToAction("Index", "Dashboard", new { recipeAdded = true });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recipe = recipeRepository.GetById(id);

            if (recipe == null) { return View("Error"); }

            var recipeVM = new EditRecipeViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,

            };

            return View(recipeVM);
        }

        [HttpPost]
        public IActionResult Edit(EditRecipeViewModel recipeVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", recipeVM);
            }

            var existingRecipe = recipeRepository.GetById(recipeVM.Id);

            if (existingRecipe == null)
            {
                return NotFound();
            }

            existingRecipe.Name = recipeVM.Name;

            recipeRepository.Save();

            return RedirectToAction("Index", "Dashboard", new { recipeAdded = true });
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var recipe = recipeRepository.GetById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var recipe = recipeRepository.GetById(id);

            if (recipe == null) { return View("Error"); }

            if (recipeService.HasAssociatedDiaryEntries(recipe))
            {
                TempData["ErrorMessage"] = "Cannot delete this recipe. It is associated with diary entry.";
                return RedirectToAction("Index", "Dashboard", new { recipeAdded = true });
            }

            recipeRepository.Delete(recipe);

            return RedirectToAction("Index", "Dashboard", new { recipeAdded = true });
        }

    }
}
