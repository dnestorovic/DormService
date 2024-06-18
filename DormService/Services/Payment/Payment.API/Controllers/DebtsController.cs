using Microsoft.AspNetCore.Mvc;
using Payment.Common.Entities;
using Payment.Common.Repository;

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

        [HttpGet("{studentID}", Name = "GetStudentDebts")]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentDebts>> GetStudentDebts(string studentID)
        {
            var studentDebts = await _repository.GetStudentDebts(studentID);
            if (studentDebts == null)
            {
                return NotFound();
            }
            return Ok(studentDebts);
        }

        [HttpPut]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateStudentDebt([FromBody] StudentDebts studentDebts)
        {
            var successfulUpdate = await _repository.UpdateStudentDebt(studentDebts);
            if (successfulUpdate)
            {
                return Ok(successfulUpdate);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<StudentDebts>), StatusCodes.Status201Created)]
        public async Task<ActionResult<StudentDebts>> CreateStudent([FromBody] StudentDebts studentDebts)
        {
            await _repository.CreateNewStudent(studentDebts);

            return CreatedAtRoute("GetStudentDebts", new { studentID = studentDebts.studentID }, studentDebts);
        }

        [HttpDelete("{studentID}")]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteStudent(string studentID)
        {
            var successfulDeletion = await _repository.DeleteStudent(studentID);
            if (successfulDeletion) {
                return Ok(successfulDeletion);
            }
            else
            {
                return NotFound();
            }
            
        }

    }
}
