using Documentation.API.Entities;
using Documentation.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Documentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentationListController : ControllerBase
    {
        IDocumentationListRepository _repository;

        public DocumentationListController(IDocumentationListRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
        public async Task<ActionResult> GetDocument(string studentId, string documentName)
        {
            var doc = await _repository.GetDocument(studentId, documentName);
            if (doc is null)
            {
                return Ok(null);
            }
            byte[] pdfBytes = doc.Content;

            // Check if pdfBytes is not null and contains data
            if (pdfBytes != null && pdfBytes.Length > 0)
            {
                // Return the PDF file as a FileStreamResult
                return File(pdfBytes, "application/pdf", doc.Title);
            }
            else
            {
                // Handle case where pdfBytes is null or empty
                return NotFound();
            }
        }


        [HttpPost("upload/{studentId}/{documentName}")]
        public async Task<IActionResult> UploadDocument(string studentId, string documentName, IFormFile file, [FromForm] string title)
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
                    Title = title,
                    Content = fileBytes
                };

               var res =  await _repository.AddDocument(studentId, document, documentName);

                return Ok("Document uploaded successfully");
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