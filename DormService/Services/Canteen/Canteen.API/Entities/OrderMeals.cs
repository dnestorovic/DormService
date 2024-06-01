namespace Canteen.API.Entities
{
    public class OrderMeals
    {
        public string Username { get; set; }
        public List<OrderMealsItem> Items { get; set; } = new List<OrderMealsItem>();

        public OrderMeals()
        {
        }

        public OrderMeals(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.MealPrice * item.NumberOfMeals;
                }
                return totalPrice;
            }
        }
    }
}
