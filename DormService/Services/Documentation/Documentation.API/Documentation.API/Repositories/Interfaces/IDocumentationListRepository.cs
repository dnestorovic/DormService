using Documentation.API.Entities;

namespace Documentation.API.Repositories.Interfaces
{
    public interface IDocumentationListRepository
    {
        Task<DocumentationList> GetListAsync(string studentId);

    }
}
