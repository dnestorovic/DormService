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

        public object GetPropertyValue(object obj, string propertyName)
        {
            propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on object of type '{obj.GetType().Name}'.");
            }
            return propertyInfo.GetValue(obj, null);
        }
        public void SetPropertyValue(object obj, string propertyName, object value)
        {
            propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Object instance is null.");
            }

            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on object '{obj.GetType().Name}'.");
            }

            // Check if the property is writable
            if (!property.CanWrite)
            {
                throw new ArgumentException($"Property '{propertyName}' on object '{obj.GetType().Name}' does not have a setter.");
            }

            // Set the property value
            property.SetValue(obj, value);
        }

        public async Task<DocumentationList?> GetDocumentList(string studentID)
        {
            var res = await _context.DocumentationList.Find(el=>el.studentID == studentID).FirstOrDefaultAsync();
            if (res == null)
            {
                DocumentationList newDocumentation = new DocumentationList(studentID);
                await _context.DocumentationList.InsertOneAsync(newDocumentation);
                return newDocumentation;
            }
            return res;
        }
        public async Task<Document?> GetDocument(string StudentID, string documentName)
        {
            DocumentationList documentationList = await _context.DocumentationList.Find(el => el.studentID == StudentID).FirstOrDefaultAsync<DocumentationList>();

            Document doc = (Document)GetPropertyValue(documentationList, documentName);
            return doc;
        }

        public async Task<bool> AddDocument(string StudentID, Document document, string documentName)
        {
            DocumentationList documentationList = await _context.DocumentationList.Find(el => el.studentID == StudentID).FirstOrDefaultAsync<DocumentationList>();
            Document doc = (Document)GetPropertyValue(documentationList, documentName);

            try{
                SetPropertyValue(documentationList, documentName, document);
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.studentID == StudentID, documentationList);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch(ArgumentException ex)
            {
                return false;
            }
           
        }

        public async Task<bool> DeleteDocument(string StudentID, string documentName)
        {
            DocumentationList documentationList = await _context.DocumentationList.Find(el => el.studentID == StudentID).FirstOrDefaultAsync<DocumentationList>();
            Document doc = (Document)GetPropertyValue(documentationList, documentName);

            try
            {
                SetPropertyValue(documentationList, documentName, null);
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.studentID == StudentID, documentationList);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
           
        }
    }
}
