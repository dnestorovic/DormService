using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Payment.Common.Entities;

namespace Payment.Common.Data
{
    public class DebtsContext : IDebtsContext
    {
        // Debts for all students
        public IMongoCollection<StudentDebts> allDebts { get; }

        public DebtsContext(IConfiguration configuration) {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionSettings"));
            var database = client.GetDatabase("DebtsDB");

            allDebts = database.GetCollection<StudentDebts>("Debts");
            StudentDebtsContextSeed.SeedData(allDebts);
        }

    }
}
