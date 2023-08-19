using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodLogger.Controllers
{

    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IFoodRepository foodRepository;
        private readonly IDiaryRepository diaryRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RecipeController(IRecipeRepository recipeRepository, IHttpContextAccessor httpContextAccessor, 
            IFoodRepository foodRepository, IDiaryRepository diaryRepository)
        {
            this.recipeRepository = recipeRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.foodRepository = foodRepository;
            this.diaryRepository = diaryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await recipeRepository.GetAll();
            return View(recipes);
        }

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
                if (food != null)
                {
                    newRecipe.Foods.Add(food);
                }
            }

            recipeRepository.Create(newRecipe);

            TempData["RecipeAdded"] = true;

            return RedirectToAction("Index", "Dashboard", new { recipeAdded = true });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var recipe = await recipeRepository.GetById(id);
            if (recipe == null) { return View("Error"); }

            if (HasAssociatedDiaryEntries(recipe))
            {
                TempData["ErrorMessage"] = "Cannot delete this recipe. It is associated with diary entry.";
                return RedirectToAction("Index", "Dashboard");
            }

            recipeRepository.Delete(recipe);
            return RedirectToAction("Index", "Dashboard");
        }
        private bool HasAssociatedDiaryEntries(Recipe recipe)
        {

            return diaryRepository.GetAllEntriesByRecipeId(recipe.Id).Any();
        }
    }
}
