using Documentation.API.Entities;

namespace Documentation.API.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> GetDocument(int documentId);
        Task AddDocument(Document document);

        Task DeleteDocument(int documentId);   
    }
}
