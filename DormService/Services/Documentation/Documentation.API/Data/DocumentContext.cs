﻿using MongoDB.Driver;
using Documentation.API.Entities;


namespace Documentation.API.Data
{
    public class DocumentContext : IDocumentContext
    {
        public DocumentContext() { 
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("DocumentationDB");

            Documents = database.GetCollection<Document>("Documents");
            DocumentationList = database.GetCollection<DocumentationList>("DocumentationList");

        }
        public IMongoCollection<Document> Documents { get; }
        public IMongoCollection<DocumentationList> DocumentationList { get; }

    }
}
