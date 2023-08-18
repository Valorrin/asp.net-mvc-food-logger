using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Models.ExternalData;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Security.Claims;

namespace FoodLogger.Controllers
{
    public class ExternalDataController : Controller
    {
        private const string ApiKey = "a8da026f64f75fa53c487993d6481f63";
        private const string AppId = "53c01a24";

        private readonly IFoodRepository foodRepository;
        public ExternalDataController(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }

        public async Task<ActionResult> Search(string searchString)
        {
            string baseUri = $"https://api.edamam.com/api/food-database/v2/parser?q=&app_id={AppId}&app_key={ApiKey}&ingr={searchString}&nutrition-type=cooking";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseUri);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var parsedResponse = JObject.Parse(responseBody);

                    var foodItems = parsedResponse["parsed"]
                        .Select(item => new FoodViewModel
                        {
                            Name = item["food"]["label"].ToString(),
                            Calories = (double)item["food"]["nutrients"]["ENERC_KCAL"],
                            Protein = (double)item["food"]["nutrients"]["PROCNT"],
                            Fat = (double)item["food"]["nutrients"]["FAT"],
                            Carbs = (double)item["food"]["nutrients"]["CHOCDF"]
                        }).ToList();

                    var foodsQuery = new FoodSearchQueryModel
                    {
                        Foods = foodItems,
                        SearchTerm = searchString
                    };

                    return View(foodsQuery);
                }
                else
                {
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public IActionResult Create(EdFoodViewModel foodToAdd)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Map the FoodViewModel data to your Food model
                var newFood = new Food
                {
                    Name = foodToAdd.Name,
                    Grams = 100,
                    Calories = foodToAdd.Calories,
                    Protein = foodToAdd.Protein,
                    Carbs = foodToAdd.Carbs,
                    Fat = foodToAdd.Fat,
                    AppUserId = currentUserId,
                    
                };

                // Add the new food item to your database using your repository or context
                foodRepository.Create(newFood);

                TempData["SuccessMessage"] = "Food item added successfully.";
                // You can redirect to a different page, show a success message, etc.
                // For example, redirecting to a listing page for all foods
                return RedirectToAction("Index", "Dashboard");
            }

            // If the model state is not valid, return to the same view
            // You might want to handle this differently based on your application's needs
            return View("Error");
        }
    }
}
