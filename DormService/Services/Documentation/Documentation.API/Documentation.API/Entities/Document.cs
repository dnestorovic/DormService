using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Documentation.API.Entities
{
    public class Document
    {

        [BsonId]
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public byte[] Content { get; set; }

    }
}
