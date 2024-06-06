using Documentation.API.Data;
using Documentation.API.Entities;
using MongoDB.Driver;

namespace Documentation.API.Repositories.Interfaces
{
    public class DocumentationListRepository : IDocumentationListRepository
    {

        private readonly IDocumentContext _context;

        public DocumentationListRepository(IDocumentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DocumentationList> GetDocumentList(string studentId)
        {
            return await _context.DocumentationList.Find(el=>el.checkStudentId(studentId)).FirstAsync();
        }

    }
}
