using Canteen.API.UserMealsInfo.Entities;
using Canteen.API.UserMealsInfo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Canteen.API.UserMealsInfo.Contorllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserMealsController : ControllerBase
    {
        private readonly IUserMealsRepository _repository;

        public UserMealsController(IUserMealsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserMeals>> GetMealsForUser(string username)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != username)
            {
                return Forbid();
            }

            var userMeals = await _repository.GetUserMeals(username);
            if (userMeals == null)
            {
                return NotFound();
            }
            return Ok(userMeals);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateUserMeals([FromBody] UserMeals userMeals)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != userMeals.Username)
            {
                return Forbid();
            }

            return Ok(await _repository.UpdateUserMeals(userMeals));
        }
    }
}
