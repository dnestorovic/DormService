using Canteen.API.OrderMealsInfo.Entities;
using Canteen.API.OrderMealsInfo.Repositories;
using Canteen.API.UserMealsInfo.Entities;
using Canteen.API.UserMealsInfo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Canteen.API.OrderMealsInfo.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderMealsController : ControllerBase
    {
        private readonly IOrderMealsRepository _repository;
        private readonly IUserMealsRepository _userMealsRepository;

        public OrderMealsController(IOrderMealsRepository repository, IUserMealsRepository userMealsRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userMealsRepository = userMealsRepository ?? throw new ArgumentNullException(nameof(userMealsRepository));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(OrderMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderMeals>> GetOrder(string username)
        {
            var order = await _repository.GetOrder(username);
            return Ok(order ?? new OrderMeals(username));
        }

        [HttpPut]
        [ProducesResponseType(typeof(OrderMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderMeals>> UpdateOrder([FromBody] OrderMeals order)
        {
            return Ok(await _repository.UpdateOrder(order));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteOrder(string username)
        {
            await _repository.DeleteOrder(username);
            return Ok();

        }


        [Route("[action]")]
        [HttpHead]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserMeals>> Checkout(string username)
        {
            var order = await _repository.GetOrder(username);
            if (order == null)
            {
                return BadRequest();
            }

            List<OrderMealsItem> orderMealItems = order.Items;
            var breakfastNum = 0;
            var lunchNum = 0;
            var dinnerNum = 0;

            foreach (var item in orderMealItems)
            {
                if (item.MealType == "Breakfast")
                {
                    breakfastNum = item.NumberOfMeals;
                }

                if (item.MealType == "Lunch")
                {
                    lunchNum = item.NumberOfMeals;
                }

                if (item.MealType == "Dinner")
                {
                    dinnerNum = item.NumberOfMeals;
                }
            }


            var mealsToAdd = new UserMeals()
            {
                Username = username,
                Breakfast = breakfastNum,
                Lunch = lunchNum,
                Dinner = dinnerNum

            };

            await _userMealsRepository.UpdateUserMeals(mealsToAdd);


            return Accepted();

        }
    }
}
