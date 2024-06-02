using MongoDB.Driver;
using Payment.API.Entities;

namespace Payment.API.Data
{
    public interface IDebtsContext
    {
        IMongoCollection<StudentDebts> allDebts { get; }
    }
}
