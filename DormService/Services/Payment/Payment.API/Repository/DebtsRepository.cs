using MongoDB.Driver;
using Payment.API.Data;
using Payment.API.Entities;

using System;
using System.Reflection;

namespace Payment.API.Repository
{
    public class DebtsRepository : IDebtsRepository
    {
        private readonly IDebtsContext _context;

        public DebtsRepository(IDebtsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        // Get debts for one student by his studentID
        public async Task<StudentDebts> GetStudentDebts(string studentID)
        {
            var studentDebts = await _context.allDebts.Find(s => s.studentID == studentID).FirstOrDefaultAsync();

            // During the first access, debts are initialized by default values
            if (studentDebts == null)
            {
                return await CreateNewStudent(studentID);
            }

            return studentDebts;
        }

        // Update one type of debt when the payment is made
        // Only one attribute is not equal to zero
        // NOTE: Student can overpay his debts
        public async Task<bool> UpdateStudentDebt(StudentDebts studentDebts)
        {
            var newStudentDebts = await GetStudentDebts(studentDebts.studentID);
            newStudentDebts.credit += studentDebts.credit;
            newStudentDebts.rent -= studentDebts.rent;
            newStudentDebts.internet -= studentDebts.internet;
            newStudentDebts.airConditioning -= studentDebts.airConditioning;
            newStudentDebts.phone -= studentDebts.phone;
            newStudentDebts.cleaning -= studentDebts.cleaning;

            var updateResult = await _context.allDebts.ReplaceOneAsync(s => s.studentID == studentDebts.studentID, newStudentDebts);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        // Creating a student with default or customized debts
        // Default value for debts is zero
        public async Task CreateNewStudent(StudentDebts studentDebts)
        {
            await _context.allDebts.InsertOneAsync(studentDebts);
        }

        // Creating a student with default debts
        public async Task<StudentDebts> CreateNewStudent(string studentID)
        {
            var newStudent = new StudentDebts(studentID);
            await _context.allDebts.InsertOneAsync(newStudent);

            return await _context.allDebts.Find(s => s.studentID == studentID).FirstOrDefaultAsync();
        }

        // Deleting a student by his studentID
        public async Task<bool> DeleteStudent(string studentID)
        {
            var deleteResult = await _context.allDebts.DeleteOneAsync(s => s.studentID == studentID);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
