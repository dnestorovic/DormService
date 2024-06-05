﻿using Documentation.API.Entities;
using Documentation.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Documentation.API.Controllers
{
    public class DocumentController : ControllerBase
    {
        IDocumentRepository _repository;

        public DocumentController(IDocumentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Document> GetDocument(int id)
        {
            return await _repository.GetDocument(id);
        }
        public async Task AddDocument(Document document)
        {
            await _repository.AddDocument(document);
        }

        public async Task DeleteDocument(int documentId)
        {
            await _repository.DeleteDocument(documentId);
        }

    }
}
