using Documentation.API.Entities;
using Documentation.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Documentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _repository;

        public DocumentController(IDocumentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentationList), StatusCodes.Status200OK)]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var doc = await _repository.GetDocument(id);
            return Ok(doc);
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> AddDocument(Document document)
        {
            await _repository.AddDocument(document);
            return Ok();
        }

        [HttpDelete("{documentId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteDocument(int documentId)
        {
            await _repository.DeleteDocument(documentId);
            return Ok();
        }

    }
}
