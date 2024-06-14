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
            return Ok(docList);
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
            if (documents.UnemploymenyCertificate is null)
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