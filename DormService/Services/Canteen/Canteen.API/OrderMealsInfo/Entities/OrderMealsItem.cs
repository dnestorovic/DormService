namespace Canteen.API.OrderMealsInfo.Entities
{
    public class OrderMealsItem
    {
        private readonly Dictionary<string, decimal> MealTypeToPrice = new Dictionary<string, decimal>()
        {
            {"Breakfast", 90}, {"Lunch", 120}, {"Dinner", 80},
        };

        public int NumberOfMeals { get; set; }
        public string MealType { get; set; }
        public decimal MealPrice
        {
            get { return MealTypeToPrice[MealType]; }
        }

    }
}
