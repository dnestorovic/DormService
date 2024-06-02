using Canteen.API.UserMealsInfo.Entities;
using Canteen.API.UserMealsInfo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Canteen.API.UserMealsInfo.Contorllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserMealsController : ControllerBase
    {
        private readonly IUserMealsRepository _repository;

        public UserMealsController(IUserMealsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("username")]
        [ProducesResponseType(typeof(UserMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserMeals>> GetMealsForUser(string username)
        {
            var userMeals = await _repository.GetUserMeals(username);
            if (userMeals == null)
            {
                return NotFound();
            }
            return Ok(userMeals);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateUsesMeals([FromBody] UserMeals userMeals)
        {
            return Ok(await _repository.UpdateUserMeals(userMeals));
        }
    }
}
