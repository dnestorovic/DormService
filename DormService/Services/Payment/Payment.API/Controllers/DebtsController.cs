using Microsoft.AspNetCore.Mvc;
using Payment.API.Entities;
using Payment.API.Repository;
using System.Diagnostics;

namespace Payment.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtsRepository _repository;

        public DebtsController(IDebtsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("studentID")]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentDebts>> GetMealsForUser(string studentID)
        {
            var studentDebts = await _repository.GetStudentDebts(studentID);
            if (studentDebts == null)
            {
                return NotFound();
            }
            return Ok(studentDebts);
        }
    }
}
