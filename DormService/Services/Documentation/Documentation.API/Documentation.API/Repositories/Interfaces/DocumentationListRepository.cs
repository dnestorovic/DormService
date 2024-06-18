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

        public async Task<DocumentationList?> GetDocumentList(string StudentID)
        {
            return await _context.DocumentationList.Find(el=>el.StudentID == StudentID).FirstAsync();
        }
        public async Task<Document?> GetDocument(string StudentID, string documentName)
        {
            DocumentationList documentationList = await _context.DocumentationList.Find(el => el.StudentID == StudentID).FirstOrDefaultAsync<DocumentationList>();
            if (documentName == "Application Form")
                return documentationList.ApplicationForm;
            if (documentName == "Income Certificate")
                return documentationList.IncomeCertificate;
            if (documentName == "Unemployment Certificate")
                return documentationList.UnemploymentCertificate;
            if (documentName == "First Time Student Certificate")
                return documentationList.FirstTimeStudentCertificate;
            if (documentName == "High School First Grade Certificate")
                return documentationList.HighSchoolFirstYearCertificate;
            if (documentName == "High School Second Grade Certificate")
                return documentationList.HighSchoolSecondYearCertificate;
            if (documentName == "High School Third Grade Certificate")
                return documentationList.HighSchoolThirdYearCertificate;
            if (documentName == "High School Fourth Grade Certificate")
                return documentationList.HighSchoolFourthYearCertificate;
            if (documentName == "Faculty Data Form")
                return documentationList.FacultyDataForm;
            if (documentName == "Average Grade Certificate")
                return documentationList.AvgGradeCertificate;
            return null;
        }

        public async Task<bool> AddDocument(string StudentID, Document document)
        {
            DocumentationList doc = await _context.DocumentationList.Find(el => el.StudentID == StudentID).FirstOrDefaultAsync<DocumentationList>();
            if (document.Title == "Application Form") {
                doc.ApplicationForm = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (document.Title == "Income Certificate") {
                doc.IncomeCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; 
            }
            if (document.Title == "Unemployment Certificate")
            {
                doc.UnemploymentCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (document.Title == "First Time Student Certificate")
            {
                doc.FirstTimeStudentCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; 
            }
            if (document.Title == "High School First Grade Certificate") {
                doc.HighSchoolFirstYearCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; 
            }
            if (document.Title == "High School Second Grade Certificate")
            {
                doc.HighSchoolSecondYearCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; 
            }
            if (document.Title == "High School Third Grade Certificate")
            {
                doc.HighSchoolThirdYearCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (document.Title == "High School Fourth Grade Certificate")
            {
                doc.HighSchoolFourthYearCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (document.Title == "Faculty Data Form")
            {
                doc.FacultyDataForm = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (document.Title == "Average Grade Certificate")
            {
                doc.AvgGradeCertificate = document;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            return false;
        }

        public async Task<bool> DeleteDocument(string StudentID, string documentName)
        {
            var doc = await _context.DocumentationList.Find(el => el.StudentID == StudentID).FirstOrDefaultAsync<DocumentationList>();
            if (documentName == "Application Form")
            {
                doc.ApplicationForm = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (documentName == "Income Certificate")
            {
                doc.IncomeCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (documentName == "Unemployment Certificate")
            {
                doc.UnemploymentCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

            }
            if (documentName == "First Time Student Certificate")
            {
                doc.FirstTimeStudentCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; 
            }
            if (documentName == "High School First Grade Certificate")
            {
                doc.HighSchoolFirstYearCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (documentName == "High School Second Grade Certificate")
            {
                doc.HighSchoolSecondYearCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (documentName == "High School Third Grade Certificate")
            {
                doc.HighSchoolThirdYearCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (documentName == "High School Fourth Grade Certificate")
            {
                doc.HighSchoolFourthYearCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (documentName == "Faculty Data Form")
            {
                doc.FacultyDataForm = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            if (documentName == "Average Grade Certificate")
            {
                doc.AvgGradeCertificate = null;
                var updateResult = await _context.DocumentationList.ReplaceOneAsync(p => p.StudentID == StudentID, doc);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            return false;
        }
    }
}
