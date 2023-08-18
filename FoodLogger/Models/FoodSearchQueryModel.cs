namespace FoodLogger.Models
{
    public class FoodSearchQueryModel
    {
        public string Name { get; set; }

        public string SearchTerm { get; set; }
        public IEnumerable<FoodViewModel> Foods { get; init; }
        public FoodViewModel FoodToAdd { get; set; }
    }

}

