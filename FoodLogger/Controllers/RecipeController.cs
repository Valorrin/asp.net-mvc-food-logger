using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodLogger.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IFoodRepository foodRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RecipeController(IRecipeRepository recipeRepository, IHttpContextAccessor httpContextAccessor, IFoodRepository foodRepository)
        {
            this.recipeRepository = recipeRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.foodRepository = foodRepository;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await recipeRepository.GetAll();
            return View(recipes);
        }

        public async Task<IActionResult> GetRecipesPartial()
        {
            var recipes = await recipeRepository.GetAll();
            return PartialView("_RecipesPartial", recipes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var availableFoods = foodRepository.GetAllFoods().ToList();
            var curUserId = httpContextAccessor.HttpContext.User.GetUserId();

            var createRecipeViewModel = new CreateRecipeViewModel
            {
                AppUserId = curUserId,
                AvailableFoods = availableFoods,
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
            return RedirectToAction("Index");
        }
    }
}
