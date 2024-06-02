using Canteen.API.OrderMealsInfo.Entities;

namespace Canteen.API.OrderMealsInfo.Repositories
{
    public interface IOrderMealsRepository
    {
        Task<OrderMeals> GetOrder(string username);
        Task<OrderMeals> UpdateOrder(OrderMeals order);
        Task DeleteOrder(string username);
    }
}
