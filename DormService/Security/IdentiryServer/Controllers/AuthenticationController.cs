using AutoMapper;
using IdentiryServer.Controllers.Base;
using IdentiryServer.DTOs;
using IdentiryServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentiryServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : RegistrationControllerBase
    {
        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
            : base(logger, mapper, userManager, roleManager)
        {
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterStudent([FromBody] NewUserDTO newUser)
        {
            return await RegisterNewUserWithRoles(newUser, new string[] { "Student" });
        }


        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAdministrator([FromBody] NewUserDTO newUser)
        {
            return await RegisterNewUserWithRoles(newUser, new string[] { "Administrator" });

        }
    }
}
