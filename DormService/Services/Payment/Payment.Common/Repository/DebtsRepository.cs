using AutoMapper;
using MongoDB.Driver;
using Payment.Common.Data;
using Payment.Common.DTOs;
using Payment.Common.Entities;

using System;
using System.Reflection;

namespace Payment.Common.Repository
{
    public class DebtsRepository : IDebtsRepository
    {
        private readonly IDebtsContext _context;
        private readonly IMapper _mapper;

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
            var newStudentDebts = await _context.allDebts.Find(s => s.studentID == studentDebts.studentID).FirstOrDefaultAsync();
            if (newStudentDebts == null)
            {
                return false;
            }

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
            var studentDebts = await _context.allDebts.Find(s => s.studentID == studentID).FirstOrDefaultAsync();
            if (studentDebts == null)
            {
                studentDebts = new StudentDebts(studentID);
                await _context.allDebts.InsertOneAsync(studentDebts);

                return await _context.allDebts.Find(s => s.studentID == studentID).FirstOrDefaultAsync();
            }

            return studentDebts;
        }

        // Deleting a student by his studentID
        public async Task<bool> DeleteStudent(string studentID)
        {
            var deleteResult = await _context.allDebts.DeleteOneAsync(s => s.studentID == studentID);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<ReduceCreditDTO> GetStudentCredit(string studentID)
        {
            var studentDebts = await _context.allDebts.Find(s => s.studentID == studentID).FirstOrDefaultAsync();

            return  _mapper.Map<ReduceCreditDTO>(studentDebts);
        }


        public async Task<bool> ReduceCredit(string studentID, int amount)
        {
            // Checking if there is enough credit
            var studentDebts = await GetStudentDebts(studentID);
            if (amount < 0 || amount > studentDebts.credit)
            {
                return false;
            }

            // Update the credit
            studentDebts.credit -= amount;

            // Update student debts with new credit balance
            var updateResult = await _context.allDebts.ReplaceOneAsync(s => s.studentID == studentDebts.studentID, studentDebts);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
