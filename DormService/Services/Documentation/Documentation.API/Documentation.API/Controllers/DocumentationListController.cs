using Documentation.API.Entities;
using Documentation.API.Repositories.Interfaces;
using Mailing;
using Mailing.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Documentation.API.Controllers
{
    

    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors("AllowSpecificOrigin")] // Apply CORS policy to this controller
    public class DocumentationListController : ControllerBase
    {
        IDocumentationListRepository _repository;
        IEmailService _emailService;

        public DocumentationListController(IDocumentationListRepository repository, IEmailService emailService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(typeof(DocumentationList), StatusCodes.Status200OK)]
        public async Task<ActionResult<DocumentationList>> GetDocumentListForStudent(string studentId)
        {
            var docList = await _repository.GetDocumentList(studentId);
            if (docList is null)
            {
                return NotFound();
            }
            return Ok(docList);
        }

        [HttpGet("get-document/{studentId}/{documentName}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocument(string studentId, string documentName)
        {
            var doc = await _repository.GetDocument(studentId, documentName);
            if (doc == null || doc.Content == null || doc.Content.Length == 0)
            {
                return NotFound();
            }

            return File(doc.Content, "application/pdf", doc.Title);
        }

        [HttpPost("upload/{studentId}/{emailAddress}")]
        public async Task<IActionResult> UploadDocument(string studentId, string emailAddress, IFormFile file, [FromForm] string title)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File is empty or missing");
                }

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    var document = new Document
                    {
                        Title = file.FileName,
                        Content = fileBytes
                    };

                    var res = await _repository.AddDocument(studentId, document, title);

                    Email email = new(emailAddress, "You have successfully submitted " + document.Title, "Documentation submission for student dorm");

                    var emailSent = await _emailService.SendEmail(email);
                    if (!emailSent)
                    {
                        await _emailService.SendEmail(email);
                    }
                    return Ok("Document uploaded successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("delete/{studentId}/{documentName}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteDocument(string studentId, string documentName)
        {
            var res = await _repository.DeleteDocument(studentId, documentName);
            if (!res)
            {
                return NotFound();
            }
            return Ok();
        }


        [HttpGet("get-missing/{studentId}")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> GetMissingDocumentsForStudent(string studentId)
        {
 
            var documents =  await _repository.GetDocumentList(studentId);
            var missingDocuments = GetNullFieldNames(documents);

            return Ok(missingDocuments);

           
        }

        public static List<string> GetNullFieldNames(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var nullFields = new List<string>();

            // Get all properties of the object
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.GetValue(obj) == null)
                {
                    nullFields.Add(property.Name);
                }
            }

            // Get all fields of the object
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetValue(obj) == null)
                {
                    nullFields.Add(field.Name);
                }
            }

            return nullFields;
        }

    }
}