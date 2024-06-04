﻿using Payment.API.Entities;

namespace Payment.API.Repository
{
    public interface IDebtsRepository
    {
        Task<StudentDebts> GetStudentDebts(string studentID);
        Task<bool> UpdateStudentDebt(StudentDebts studentDebts);
        Task CreateNewStudent(StudentDebts studentDebts);
        Task<StudentDebts> CreateNewStudent(string studentID);
        Task<bool> DeleteStudent(string studentID);
    }
}
