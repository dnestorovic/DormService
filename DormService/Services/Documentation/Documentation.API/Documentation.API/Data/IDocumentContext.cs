using Documentation.API.Entities;
using MongoDB.Driver;

namespace Documentation.API.Data
{
    public interface IDocumentContext
    {
        public IMongoCollection<Document> Documents { get; }
    }
}
