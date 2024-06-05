using MongoDB.Driver;
using Payment.Common.Entities;

namespace Payment.Common.Data
{
    public interface IDebtsContext
    {
        IMongoCollection<StudentDebts> allDebts { get; }
    }
}
