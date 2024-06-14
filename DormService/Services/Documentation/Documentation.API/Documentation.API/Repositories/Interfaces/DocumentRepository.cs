using Documentation.API.Data;
using Documentation.API.Entities;
using Newtonsoft.Json;
using MongoDB.Driver;

namespace Documentation.API.Repositories.Interfaces
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IDocumentContext _context;

        public DocumentRepository(IDocumentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Document> GetDocument(int documentId)
        {
            return await _context.Documents.Find(el => el.DocumentId == documentId).FirstOrDefaultAsync<Document>();
        }

        public async Task AddDocument(Document document)
        {
            await _context.Documents.InsertOneAsync(document);
        }

        public async Task DeleteDocument(int documentId)
        {
            await _context.Documents.DeleteOneAsync(el => el.DocumentId == documentId);
        }
    }
}
