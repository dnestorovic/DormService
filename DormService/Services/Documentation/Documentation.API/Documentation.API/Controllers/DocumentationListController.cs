using Documentation.API.Entities;
using Documentation.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("{studentId}/{documentName}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDocument(string studentId, string documentName)
        {
            var doc = await _repository.GetDocument(studentId, documentName);
            if (doc is null)
            {
                return NotFound();
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

        [HttpPut("{studentId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> AddDocument(string studentId, Document document)
        {
            var res = await _repository.AddDocument(studentId, document);
            if (!res)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{studentId}/{documentName}")]
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


        [HttpGet("{studentId}/{grade}")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> GetMissingDocumentsForStudent(string studentId, int grade)
        {
 
            var documents =  await _repository.GetDocumentList(studentId);
            var missingDocuments = new List<string>();

            if (documents.ApplicationForm is null)
                missingDocuments.Add("Application Form");
            if (documents.IncomeCertificate is null)
                missingDocuments.Add("Income Certificate");
            if (documents.UnemploymentCertificate is null)
                missingDocuments.Add("Unemployment Certificate");
            if (grade == 1)
            {
                if (documents.FirstTimeStudentCertificate is null)
                    missingDocuments.Add("First Time Student Certificate");
                if (documents.HighSchoolFirstYearCertificate is null)
                    missingDocuments.Add("High School First Grade Certificate");
                if (documents.HighSchoolSecondYearCertificate is null)
                    missingDocuments.Add("High School Second Grade Certificate");
                if (documents.HighSchoolThirdYearCertificate is null)
                    missingDocuments.Add("High School Third Grade Certificate");
                if (documents.HighSchoolFourthYearCertificate is null)
                    missingDocuments.Add("High School Fourth Grade Certificate");
            }
            else
            {
                if (documents.FacultyDataForm is null)
                    missingDocuments.Add("Faculty Data Form");
                if (documents.AvgGradeCertificate is null)
                    missingDocuments.Add("Average Grade Certificate");
            }

            return Ok(missingDocuments);

        }

    }
}