using FoodLogger.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace FoodLogger.Controllers
{
    public class EdamamController : Controller
    {
        private const string ApiKey = "a8da026f64f75fa53c487993d6481f63";
        private const string AppId = "53c01a24";

        public async Task<ActionResult> Search(string query)
        {
            string baseUri = $"https://api.edamam.com/api/food-database/v2/parser?q={query}&app_id={AppId}&app_key={ApiKey}&ingr=banana&nutrition-type=cooking";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseUri);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var parsedResponse = JObject.Parse(responseBody);

                    var foodItems = parsedResponse["parsed"]
                        .Select(item => new FoodInfoViewModel
                        {
                            Name = item["food"]["label"].ToString(),
                            Calories = item["food"]["nutrients"]["ENERC_KCAL"].ToString(),
                            Protein = item["food"]["nutrients"]["PROCNT"].ToString(),
                            Fat = item["food"]["nutrients"]["FAT"].ToString(),
                            Carbs = item["food"]["nutrients"]["CHOCDF"].ToString()
                        }).ToList();

                    return View(foodItems);
                }
                else
                {
                    return View("Error");
                }
            }
        }
    }
}
