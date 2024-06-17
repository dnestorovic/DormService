using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.Common.Entities;
using Payment.Common.Repository;
using System.Security.Claims;

namespace Payment.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtsRepository _repository;

        public DebtsController(IDebtsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [Authorize(Roles = "Student, Administrator")]
        [HttpGet("{studentID}", Name = "GetStudentDebts")]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentDebts>> GetStudentDebts(string studentID)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != studentID)
            {
                return Forbid();
            }

            var studentDebts = await _repository.GetStudentDebts(studentID);
            if (studentDebts == null)
            {
                return NotFound();
            }
            return Ok(studentDebts);
        }


        [Authorize(Roles = "Student,Administrator")]
        [HttpPut]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateStudentDebt([FromBody] StudentDebts studentDebts)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != studentDebts.studentID)
            {
                return Forbid();
            }

            return Ok(await _repository.UpdateStudentDebt(studentDebts));
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<StudentDebts>), StatusCodes.Status201Created)]
        public async Task<ActionResult<StudentDebts>> CreateStudent([FromBody] StudentDebts studentDebts)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != studentDebts.studentID)
            {
                return Forbid();
            }

            await _repository.CreateNewStudent(studentDebts);

            return CreatedAtRoute("GetStudentDebts", new { studentID = studentDebts.studentID }, studentDebts);
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{studentID}")]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteStudent(string studentID)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != studentID)
            {
                return Forbid();
            }

            return Ok(await _repository.DeleteStudent(studentID));
        }
    }
}
