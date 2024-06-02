using Canteen.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Canteen.API.Repositories
{
    public class OrderMealsRepository : IOrderMealsRepository
    {

        private readonly IDistributedCache _cache;

        public OrderMealsRepository(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OrderMeals> GetOrder(string username)
        {
            var order = await _cache.GetStringAsync(username);
            if (string.IsNullOrEmpty(order))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<OrderMeals>(order);
        }

        public async Task<OrderMeals> UpdateOrder(OrderMeals order)
        {
            var orderString = JsonConvert.SerializeObject(order);
            await _cache.SetStringAsync(order.Username, orderString);
            return await GetOrder(order.Username);
        }

        public async Task DeleteOrder(string username)
        {
            await _cache.RemoveAsync(username);
        }


    }
}
