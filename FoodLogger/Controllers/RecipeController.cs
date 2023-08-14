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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var availableFoods = foodRepository.GetAllFood().ToList();
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
                createRecipeViewModel.AvailableFoods = foodRepository.GetAllFood().ToList();
                return View(createRecipeViewModel);
            }

            var newRecipe = new Recipe
            {
                Name = createRecipeViewModel.Name,
                RecipeFoods = createRecipeViewModel.SelectedFoodIds
                    .Where(foodId => foodId > 0)
                    .Select(foodId => new RecipeFood
                    {
                        FoodId = foodId
                    })
                    .ToList()
            };

            recipeRepository.Create(newRecipe);
            return RedirectToAction("Index");
        }
    }
}
