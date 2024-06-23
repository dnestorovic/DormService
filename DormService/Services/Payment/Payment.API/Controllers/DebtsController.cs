using Mailing;
using Mailing.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.Common.Entities;
using Payment.Common.Repository;

namespace Payment.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtsRepository _repository;
        private readonly IEmailService _emailService;

        public DebtsController(IDebtsRepository repository, IEmailService emailService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [Authorize(Roles = "Student, Administrator")]
        [HttpGet("{studentID}", Name = "GetStudentDebts")]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentDebts>> GetStudentDebts(string studentID)
        {
            var studentDebts = await _repository.GetStudentDebts(studentID);
            return Ok(studentDebts);
        }


        [Authorize(Roles = "Student,Administrator")]
        [HttpPut]
        [ProducesResponseType(typeof(StudentDebts), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateStudentDebt([FromBody] StudentDebts studentDebts, string emailAddress)
        {
            
            var successfulUpdate = await _repository.UpdateStudentDebt(studentDebts);
            if (successfulUpdate)
            {
                Email email = new(emailAddress, "Hi " + studentDebts.studentID + ",\nYou have successfully completed the transaction! \nRegards,\nDormService", "Payment confirmation");
                var emailSent = await _emailService.SendEmail(email);
                if (!emailSent)
                {
                    await _emailService.SendEmail(email);
                }

                return Ok(successfulUpdate);
            }
            else
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<StudentDebts>), StatusCodes.Status201Created)]
        public async Task<ActionResult<StudentDebts>> CreateStudent([FromBody] StudentDebts studentDebts)
        {

            await _repository.CreateNewStudent(studentDebts);
            return CreatedAtRoute("GetStudentDebts", new { studentID = studentDebts.studentID }, studentDebts);
        }


        [Authorize(Roles = "Administrator")]
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
