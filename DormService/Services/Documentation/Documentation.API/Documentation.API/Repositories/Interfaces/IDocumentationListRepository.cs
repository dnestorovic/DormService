using Documentation.API.Entities;

namespace Documentation.API.Repositories.Interfaces
{
    public interface IDocumentationListRepository
    {
        Task<DocumentationList?> GetDocumentList(string studentId);
        Task<Document?> GetDocument(string studentId, string documentName);
        Task<bool> AddDocument(string studentId, Document document);
        Task<bool> DeleteDocument(string studentId, string documentName);



    }
}
