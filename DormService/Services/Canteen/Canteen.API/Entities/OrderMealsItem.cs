namespace Canteen.API.Entities
{
    public class OrderMealsItem
    {
        private readonly Dictionary<string, decimal> MealTypeToPrice = new Dictionary<string, decimal>()
        {
            {"breakfast", 90}, {"lunch", 120}, {"dinner", 80},
        };

        public int NumberOfMeals { get; set; }
        public string MealType { get; set; }
        public decimal MealPrice
        {
            get
            {
                return MealTypeToPrice[MealType] = MealPrice;
            }
        }
    }
}
