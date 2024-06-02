using Canteen.API.Entities;
using Canteen.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Canteen.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderMealsController : ControllerBase
    {
        private readonly IOrderMealsRepository _repository;

        public OrderMealsController(IOrderMealsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
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
    }
}
