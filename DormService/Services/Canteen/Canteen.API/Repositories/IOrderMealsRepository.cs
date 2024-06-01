using Canteen.API.Entities;

namespace Canteen.API.Repositories
{
    public interface IOrderMealsRepository
    {
        Task<OrderMeals> GetOrder(string username);
        Task<OrderMeals> UpdateOrder(OrderMeals order);
        Task DeleteOrder(string username);
    }
}
