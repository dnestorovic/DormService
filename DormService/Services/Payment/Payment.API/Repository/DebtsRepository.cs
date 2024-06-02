using MongoDB.Driver;
using Payment.API.Data;
using Payment.API.Entities;

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
    }
}
