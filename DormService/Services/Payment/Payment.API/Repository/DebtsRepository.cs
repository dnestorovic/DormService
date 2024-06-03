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
            return await _context.allDebts.Find(s => s.studentID == studentID).FirstOrDefaultAsync();
        }

        // Update one type of debt when the payment is made
        // Only one attribute is not equal to zero
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
    }
}
