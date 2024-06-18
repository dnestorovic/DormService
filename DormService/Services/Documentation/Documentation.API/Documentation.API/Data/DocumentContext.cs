using MongoDB.Driver;
using Documentation.API.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Documentation.API.Data
{
    public class DocumentContext : IDocumentContext
    {
        public DocumentContext(IConfiguration configuration) {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionSettings"));
            var database = client.GetDatabase("DocumentationDB");

            DocumentationList = database.GetCollection<DocumentationList>("DocumentationList");

        }
        public IMongoCollection<DocumentationList> DocumentationList { get; }

    }
}
